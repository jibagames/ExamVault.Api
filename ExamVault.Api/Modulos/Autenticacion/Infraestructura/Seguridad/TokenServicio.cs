using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExamVault.Api.Modulos.Autenticacion.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Autenticacion.Dominio.Entidades;
using Microsoft.IdentityModel.Tokens;

namespace ExamVault.Api.Modulos.Autenticacion.Infraestructura.Seguridad
{
    public class TokenServicio : ITokenServicio
    {
        private readonly IConfiguration _configuracion;

        public TokenServicio(IConfiguration configuracion)
        {
            _configuracion = configuracion;
        }

        public string GenerarToken(Usuario usuario, IList<string> roles)
        {
            var reclamos = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email, usuario.Correo)
            };

            foreach (var rol in roles)
            {
                reclamos.Add(new Claim(ClaimTypes.Role, rol));
            }

            var llaveSecreta = _configuracion["Jwt:Key"] ?? string.Empty;
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(llaveSecreta));
            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var descriptorToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(reclamos),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _configuracion["Jwt:Issuer"],
                Audience = _configuracion["Jwt:Audience"],
                SigningCredentials = credenciales
            };

            var manejadorToken = new JwtSecurityTokenHandler();
            var token = manejadorToken.CreateToken(descriptorToken);

            return manejadorToken.WriteToken(token);
        }
    }
}
