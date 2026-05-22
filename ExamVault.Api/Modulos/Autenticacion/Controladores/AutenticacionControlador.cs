using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using ExamVault.Api.Infraestructura.Datos;
using ExamVault.Api.Dominio.Entidades;
using ExamVault.Api.Modulos.Autenticacion.DTOs;

namespace ExamVault.Api.Modulos.Autenticacion.Controladores
{
    [Route("api/Autenticacion")]
    [ApiController]
    public class AutenticacionControlador : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AutenticacionControlador(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registrar([FromBody] RegistroDto peticion)
        {
            if (string.IsNullOrWhiteSpace(peticion.Correo) || !peticion.Correo.Contains('@'))
            {
                return BadRequest(new { mensaje = "El correo es obligatorio y debe tener un formato válido." });
            }

            var partesCorreo = peticion.Correo.Split('@');
            
            if (partesCorreo.Length != 2 || string.IsNullOrWhiteSpace(partesCorreo[1]))
            {
                return BadRequest(new { mensaje = "Formato de correo inválido o sin dominio." });
            }
            
            var dominio = partesCorreo[1].Trim().ToLower();

            var institucion = await _context.Instituciones
                .FirstOrDefaultAsync(i => i.DominioCorreo.ToLower() == dominio && i.Estado == "ACTIVO");

            if (institucion == null)
            {
                return BadRequest(new { mensaje = "El dominio del correo no pertenece a una institución registrada o activa." });
            }

            var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo.ToLower() == peticion.Correo.ToLower());

            if (usuarioExistente != null)
            {
                return BadRequest(new { mensaje = "El correo ya está en uso." });
            }

            var rolEstudiante = await _context.Roles
                .FirstOrDefaultAsync(r => r.NombreRol == "Estudiante");

            if (rolEstudiante == null)
            {
                return StatusCode(500, new { mensaje = "Error de configuración: El rol predeterminado no existe en el sistema." });
            }

            using var transaccion = await _context.Database.BeginTransactionAsync();

            try
            {
                var nuevoUsuario = new Usuario
                {
                    PrimerNombre = peticion.PrimerNombre,
                    SegundoNombre = peticion.SegundoNombre,
                    Apellidos = peticion.Apellidos,
                    Correo = peticion.Correo,
                    ContrasenaHash = BCrypt.Net.BCrypt.HashPassword(peticion.Contrasena),
                    Estado = "ACTIVO",
                    IdInstituciones = institucion.IdInstituciones
                };

                _context.Usuarios.Add(nuevoUsuario);
                await _context.SaveChangesAsync();

                var usuarioRol = new UsuarioRol
                {
                    IdUsuario = nuevoUsuario.IdUsuario,
                    IdRol = rolEstudiante.IdRol
                };

                _context.UsuariosRoles.Add(usuarioRol);

                var ipCliente = HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Desconocida";

                var registroAuditoria = new Auditoria
                {
                    IdUsuario = nuevoUsuario.IdUsuario,
                    AccionAuditada = "REGISTRO",
                    Detalle = $"Usuario registrado con correo {nuevoUsuario.Correo} bajo el rol ESTUDIANTE",
                    IpOrigen = ipCliente,
                    FechaAccion = DateTime.UtcNow
                };

                _context.Auditoria.Add(registroAuditoria);

                await _context.SaveChangesAsync();
                await transaccion.CommitAsync();

                return Ok(new { mensaje = "Usuario registrado exitosamente." });
            }
            catch (Exception)
            {
                await transaccion.RollbackAsync();
                return StatusCode(500, new { mensaje = "Error interno al procesar el registro. La operación ha sido revertida." });
            }
        }

        [HttpPost("iniciar-sesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] IniciarSesionDto peticion)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == peticion.Correo && u.Estado == "ACTIVO");

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(peticion.Contrasena, usuario.ContrasenaHash))
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

            var respuesta = new RespuestaInicioSesionDto
            {
                Token = tokenHandler.WriteToken(token),
                Usuario = new UsuarioDto
                {
                    Id = usuario.IdUsuario,
                    Correo = usuario.Correo,
                    NombreCompleto = $"{usuario.PrimerNombre} {usuario.Apellidos}".Trim(),
                    Rol = nombresRoles.FirstOrDefault() ?? string.Empty,
                    InstitucionId = usuario.IdInstituciones
                }
            };

            return Ok(respuesta);
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
        public async Task<IActionResult> AsignarRol([FromBody] AsignarRolDto peticion)
        {
            var usuarioExiste = await _context.Usuarios
                .AnyAsync(u => u.IdUsuario == peticion.IdUsuario);

            if (!usuarioExiste)
            {
                return NotFound(new { mensaje = "El usuario especificado no existe." });
            }

            var rolExiste = await _context.Roles
                .AnyAsync(r => r.IdRol == peticion.IdRol);

            if (!rolExiste)
            {
                return NotFound(new { mensaje = "El rol especificado no existe." });
            }

            var yaTieneRol = await _context.UsuariosRoles
                .AnyAsync(ur => ur.IdUsuario == peticion.IdUsuario && ur.IdRol == peticion.IdRol);

            if (yaTieneRol)
            {
                return BadRequest(new { mensaje = "El usuario ya tiene este rol asignado." });
            }

            var nuevaAsignacion = new UsuarioRol
            {
                IdUsuario = peticion.IdUsuario,
                IdRol = peticion.IdRol
            };

            _context.UsuariosRoles.Add(nuevaAsignacion);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Rol asignado exitosamente." });
        }

        [HttpPut("perfil")]
        [Authorize]
        public async Task<IActionResult> ActualizarPerfil([FromBody] ActualizarPerfilDto peticion)
        {
            var idUsuarioClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(idUsuarioClaim) || !int.TryParse(idUsuarioClaim, out int idUsuario))
            {
                return Unauthorized(new { mensaje = "Token inválido o no contiene el ID del usuario." });
            }

            var usuario = await _context.Usuarios.FindAsync(idUsuario);

            if (usuario == null || usuario.Estado != "ACTIVO")
            {
                return NotFound(new { mensaje = "Usuario no encontrado o inactivo." });
            }

            if (!string.IsNullOrWhiteSpace(peticion.PrimerNombre))
            {
                usuario.PrimerNombre = peticion.PrimerNombre;
            }

            if (!string.IsNullOrWhiteSpace(peticion.Apellidos))
            {
                usuario.Apellidos = peticion.Apellidos;
            }

            if (!string.IsNullOrWhiteSpace(peticion.FotoUrl))
            {
                usuario.FotoUrl = peticion.FotoUrl;
            }

            if (!string.IsNullOrWhiteSpace(peticion.Contacto))
            {
                usuario.Contacto = peticion.Contacto;
            }

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Perfil actualizado exitosamente." });
        }

        [HttpGet]
        [Route("instituciones-activas")]
        public async Task<IActionResult> GetInstitucionesActivas()
        {
            var instituciones = await _context.Instituciones
                .Where(i => i.Estado == "ACTIVO")
                .Select(i => new
                {
                    i.IdInstituciones,
                    i.NombreInstitucion,
                    i.DominioCorreo
                })
                .ToListAsync();

            return Ok(instituciones);
        }

        [HttpPost("cerrar-sesion")]
        [Authorize]
        public IActionResult CerrarSesion()
        {
            return Ok(new { mensaje = "Sesión cerrada. Proceda a eliminar el token en el cliente." });
        }
    }
}