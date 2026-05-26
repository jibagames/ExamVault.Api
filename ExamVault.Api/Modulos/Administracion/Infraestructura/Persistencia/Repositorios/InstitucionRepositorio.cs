using ExamVault.Api.Infraestructura.Datos;
using ExamVault.Api.Modulos.Administracion.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ExamVault.Api.Modulos.Administracion.Infraestructura.Persistencia.Repositorios
{
    public class InstitucionRepositorio : IInstitucionRepositorio
    {
        private readonly ApplicationDbContext _contexto;

        public InstitucionRepositorio(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> ExisteDominioAsync(string dominioCorreo)
        {
            return await _contexto.Instituciones.AnyAsync(i => i.DominioCorreo == dominioCorreo);
        }

        public async Task<Institucion> CrearAsync(Institucion institucion)
        {
            await _contexto.Instituciones.AddAsync(institucion);
            await _contexto.SaveChangesAsync();
            return institucion;
        }

        public async Task<IEnumerable<Institucion>> ObtenerTodasAsync()
        {
            return await _contexto.Instituciones.ToListAsync();
        }
    }
}