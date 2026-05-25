using ExamVault.Api.Infraestructura.Datos;
using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;
using ExamVault.Api.Modulos.Autenticacion.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExamVault.Api.Modulos.Autenticacion.Infraestructura.Persistencia.Repositorios
{
    public class InstitucionRepositorio : IInstitucionRepositorio
    {
        private readonly ApplicationDbContext _contexto;

        public InstitucionRepositorio(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<Institucion?> ObtenerPorDominioAsync(string dominio)
        {
            return await _contexto.Instituciones.FirstOrDefaultAsync(i => i.DominioCorreo == dominio);
        }

        public async Task<IEnumerable<dynamic>> ObtenerActivasAsync()
        {
            return await _contexto.Instituciones
                .Where(i => i.Estado == "ACTIVO")
                .Select(i => new
                {
                    i.IdInstituciones,
                    i.NombreInstitucion,
                    i.DominioCorreo
                })
                .ToListAsync();
        }
    }
}