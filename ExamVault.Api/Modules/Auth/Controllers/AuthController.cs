using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using ExamVault.Api.Infrastructure.Data;
using ExamVault.Api.Domain.Entities;

namespace ExamVault.Api.Modules.Auth.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registrar([FromBody] RegistroRequest request)
        {
            var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == request.Correo);

            if (usuarioExistente != null)
            {
                return BadRequest(new { mensaje = "El correo ya está en uso." });
            }

            var institucionExiste = await _context.Instituciones
                .AnyAsync(i => i.IdInstituciones == request.IdInstituciones);

            if (!institucionExiste)
            {
                return BadRequest(new { mensaje = "La institución especificada no existe." });
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var nuevoUsuario = new Usuario
                {
                    PrimerNombre = request.PrimerNombre,
                    Apellidos = request.Apellidos,
                    Correo = request.Correo,
                    ContrasenaHash = BCrypt.Net.BCrypt.HashPassword(request.Contrasena),
                    IdInstituciones = request.IdInstituciones,
                    Estado = "ACTIVO"
                };

                _context.Usuarios.Add(nuevoUsuario);
                await _context.SaveChangesAsync();

                var rolAdministrador = await _context.Roles.FirstOrDefaultAsync(r => r.NombreRol == "Administrador");
                var rolEstudiante = await _context.Roles.FirstOrDefaultAsync(r => r.NombreRol == "Estudiante");

                int cantidadAdmins = 0;
                if (rolAdministrador != null)
                {
                    cantidadAdmins = await _context.UsuariosRoles.CountAsync(ur => ur.IdRol == rolAdministrador.IdRol);
                }

                var rolAsignar = cantidadAdmins < 3 ? rolAdministrador : rolEstudiante;

                if (rolAsignar != null)
                {
                    var asignacionRol = new UsuarioRol
                    {
                        IdUsuario = nuevoUsuario.IdUsuario,
                        IdRol = rolAsignar.IdRol
                    };

                    _context.UsuariosRoles.Add(asignacionRol);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                string nombreRolAsignado = cantidadAdmins < 3 ? "Administrador" : "Estudiante";
                return Ok(new { mensaje = $"Usuario registrado exitosamente con rol de {nombreRolAsignado}." });
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { mensaje = "Error interno al registrar el usuario." });
            }
        }

        [HttpPost("iniciar-sesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] LoginRequest request)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == request.Correo && u.Estado == "ACTIVO");

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Contrasena, usuario.ContrasenaHash))
            {
                return Unauthorized(new { mensaje = "Credenciales incorrectas o usuario inactivo." });
            }

            var rolesDelUsuario = await _context.UsuariosRoles
                .Where(ur => ur.IdUsuario == usuario.IdUsuario)
                .Select(ur => ur.IdRol)
                .ToListAsync();

            var nombresRoles = await _context.Roles
                .Where(r => rolesDelUsuario.Contains(r.IdRol))
                .Select(r => r.NombreRol)
                .ToListAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim("IdInstitucion", usuario.IdInstituciones.ToString())
            };

            foreach (var rol in nombresRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                usuario = new
                {
                    usuario.PrimerNombre,
                    usuario.Apellidos,
                    usuario.Correo,
                    roles = nombresRoles
                }
            });
        }

        [HttpGet("verificar-sesion")]
        [Authorize]
        public IActionResult VerificarSesion()
        {
            var correo = User.FindFirstValue(ClaimTypes.Email);
            var idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

            return Ok(new
            {
                mensaje = "Sesión activa",
                idUsuario,
                correo,
                roles
            });
        }

        [HttpGet("verificar/administrador")]
        [Authorize(Roles = "Administrador")]
        public IActionResult VerificarAdministrador()
        {
            return Ok(new
            {
                mensaje = "Acceso exitoso. Tienes permisos de Administrador."
            });
        }

        [HttpPost("asignar-rol")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AsignarRol([FromBody] AsignarRolRequest request)
        {
            var usuarioExiste = await _context.Usuarios
                .AnyAsync(u => u.IdUsuario == request.IdUsuario);

            if (!usuarioExiste)
            {
                return NotFound(new { mensaje = "El usuario especificado no existe." });
            }

            var rolExiste = await _context.Roles
                .AnyAsync(r => r.IdRol == request.IdRol);

            if (!rolExiste)
            {
                return NotFound(new { mensaje = "El rol especificado no existe." });
            }

            var yaTieneRol = await _context.UsuariosRoles
                .AnyAsync(ur => ur.IdUsuario == request.IdUsuario && ur.IdRol == request.IdRol);

            if (yaTieneRol)
            {
                return BadRequest(new { mensaje = "El usuario ya tiene este rol asignado." });
            }

            var nuevaAsignacion = new UsuarioRol
            {
                IdUsuario = request.IdUsuario,
                IdRol = request.IdRol
            };

            _context.UsuariosRoles.Add(nuevaAsignacion);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Rol asignado exitosamente." });
        }
    }

    public class RegistroRequest
    {
        public string PrimerNombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public int IdInstituciones { get; set; }
    }

    public class LoginRequest
    {
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }

    public class AsignarRolRequest
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
    }
}