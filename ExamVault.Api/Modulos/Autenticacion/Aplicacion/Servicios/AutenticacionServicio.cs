using ExamVault.Api.Modulos.Autenticacion.Aplicacion.DTOs;
using ExamVault.Api.Modulos.Autenticacion.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Autenticacion.Dominio.Entidades;
using System.Text.RegularExpressions;

namespace ExamVault.Api.Modulos.Autenticacion.Aplicacion.Servicios
{
    public class AutenticacionServicio : IAutenticacionServicio
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IInstitucionRepositorio _institucionRepositorio;
        private readonly IEncriptadorServicio _encriptadorServicio;
        private readonly ITokenServicio _tokenServicio;

        public AutenticacionServicio(
            IUsuarioRepositorio usuarioRepositorio,
            IInstitucionRepositorio institucionRepositorio,
            IEncriptadorServicio encriptadorServicio,
            ITokenServicio tokenServicio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _institucionRepositorio = institucionRepositorio;
            _encriptadorServicio = encriptadorServicio;
            _tokenServicio = tokenServicio;
        }

        public async Task RegistrarAsync(RegistroDto peticion)
        {
            if (string.IsNullOrWhiteSpace(peticion.Correo))
            {
                throw new ArgumentException("El correo es obligatorio.");
            }

            string patronCorreo = @"^[a-zA-Z0-9._%+-]{3,30}@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(peticion.Correo, patronCorreo))
            {
                throw new ArgumentException("El formato del correo electrónico es inválido, contiene caracteres no permitidos o es demasiado largo.");
            }

            var partesCorreo = peticion.Correo.Split('@');
            string dominio = partesCorreo[1]; // <-- Aquí sacamos el texto específico

            var institucion = await _institucionRepositorio.ObtenerPorDominioAsync(dominio);


            if (institucion == null)
            {
                throw new ArgumentException("El dominio del correo no pertenece a ninguna institución registrada.");
            }

            var existeCorreo = await _usuarioRepositorio.ExisteCorreoAsync(peticion.Correo);

            if (existeCorreo)
            {
                throw new ArgumentException("El correo ya está registrado en el sistema.");
            }

            var usuario = new Usuario
            {
                PrimerNombre = peticion.PrimerNombre,
                SegundoNombre = peticion.SegundoNombre,
                Apellidos = peticion.Apellidos,
                Correo = peticion.Correo,
                ContrasenaHash = _encriptadorServicio.Encriptar(peticion.Contrasena),
                Estado = "ACTIVO",
                IdInstituciones = institucion.IdInstituciones
            };

            await _usuarioRepositorio.AgregarAsync(usuario);

            var usuarioRol = new UsuarioRol
            {
                IdUsuario = usuario.IdUsuario,
                IdRol = 1
            };

            await _usuarioRepositorio.AgregarRolAsync(usuarioRol);
        }

        public async Task<RespuestaInicioSesionDto> IniciarSesionAsync(IniciarSesionDto peticion)
        {
            var usuario = await _usuarioRepositorio.ObtenerPorCorreoAsync(peticion.Correo);

            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Credenciales incorrectas.");
            }

            var contrasenaValida = _encriptadorServicio.Verificar(peticion.Contrasena, usuario.ContrasenaHash);

            if (!contrasenaValida)
            {
                throw new UnauthorizedAccessException("Credenciales incorrectas.");
            }

            if (usuario.Estado != "ACTIVO")
            {
                throw new UnauthorizedAccessException("La cuenta de usuario no está activa o se encuentra suspendida.");
            }

            var roles = await _usuarioRepositorio.ObtenerRolesDeUsuarioAsync(usuario.IdUsuario);

            string rolPrincipal = "Estudiante";
            IList<string> rolesParaToken = new List<string> { rolPrincipal };

            if (roles != null && roles.Count > 0)
            {
                rolPrincipal = roles[0]; // Extraemos el primer rol
                rolesParaToken = roles;
            }

            var token = _tokenServicio.GenerarToken(usuario, rolesParaToken);

            var respuesta = new RespuestaInicioSesionDto
            {
                Token = token,
                Usuario = new UsuarioDto
                {
                    Id = usuario.IdUsuario,
                    Correo = usuario.Correo,
                    NombreCompleto = $"{usuario.PrimerNombre} {usuario.SegundoNombre} {usuario.Apellidos}".Trim(),
                    Rol = rolPrincipal,
                    InstitucionId = usuario.IdInstituciones
                }
            };

            return respuesta;
        }

        public async Task ActualizarPerfilAsync(int idUsuario, ActualizarPerfilDto peticion)
        {
            var usuario = await _usuarioRepositorio.ObtenerPorIdAsync(idUsuario);

            if (usuario == null)
            {
                throw new ArgumentException("Usuario no encontrado.");
            }

            if (!string.IsNullOrWhiteSpace(peticion.PrimerNombre))
            {
                usuario.PrimerNombre = peticion.PrimerNombre;
            }

            if (!string.IsNullOrWhiteSpace(peticion.SegundoNombre))
            {
                usuario.SegundoNombre = peticion.SegundoNombre;
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

            await _usuarioRepositorio.ActualizarAsync(usuario);
        }

        public async Task<IEnumerable<dynamic>> ObtenerInstitucionesActivasAsync()
        {
            return await _institucionRepositorio.ObtenerActivasAsync();
        }

        public async Task AsignarRolAsync(AsignarRolDto peticion)
        {
            var usuario = await _usuarioRepositorio.ObtenerPorIdAsync(peticion.IdUsuario);

            if (usuario == null)
            {
                throw new ArgumentException("El usuario especificado no existe.");
            }

            var usuarioRol = new UsuarioRol
            {
                IdUsuario = peticion.IdUsuario,
                IdRol = peticion.IdRol
            };

            await _usuarioRepositorio.AgregarRolAsync(usuarioRol);
        }
    }
}