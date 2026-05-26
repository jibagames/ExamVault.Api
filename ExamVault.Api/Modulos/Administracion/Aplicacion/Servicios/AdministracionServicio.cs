using ExamVault.Api.Modulos.Administracion.Aplicacion.DTOs;
using ExamVault.Api.Modulos.Administracion.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Administracion.Aplicacion.Servicios
{
    public class AdministracionServicio : IAdministracionServicio
    {
        private readonly IInstitucionRepositorio _repositorio;

        // Inyectamos la abstracción, no la implementación (DIP)
        public AdministracionServicio(IInstitucionRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Institucion> RegistrarInstitucionAsync(CrearInstitucionDto peticion)
        {
            // Regla de negocio: No pueden existir dos instituciones con el mismo dominio
            bool existe = await _repositorio.ExisteDominioAsync(peticion.DominioCorreo);
            if (existe)
            {
                throw new ArgumentException($"El dominio {peticion.DominioCorreo} ya está registrado.");
            }

            var nuevaInstitucion = new Institucion
            {
                NombreInstitucion = peticion.NombreInstitucion,
                DominioCorreo = peticion.DominioCorreo.ToLower(),
                Estado = "ACTIVO",
                Creado = DateTime.UtcNow
            };

            return await _repositorio.CrearAsync(nuevaInstitucion);
        }

        public async Task<IEnumerable<Institucion>> ListarInstitucionesAsync()
        {
            return await _repositorio.ObtenerTodasAsync();
        }
    }
}