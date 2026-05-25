using ExamVault.Api.Infraestructura.Datos;
using ExamVault.Api.Modulos.Repositorio.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Repositorio.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ExamVault.Api.Modulos.Repositorio.Infraestructura.Persistencia.Repositorios
{
    public class MaterialRepositorio : IMaterialRepositorio
    {
        private readonly ApplicationDbContext _contexto;

        public MaterialRepositorio(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task AgregarAsync(Material material)
        {
            await _contexto.Materiales.AddAsync(material);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Material?> ObtenerPorIdAsync(int idMaterial)
        {
            return await _contexto.Materiales.FindAsync(idMaterial);
        }

        public async Task ActualizarAsync(Material material)
        {
            _contexto.Materiales.Update(material);
            await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<Material>> ObtenerAprobadosPorMateriaAsync(int idMateria)
        {
            return await _contexto.Materiales
                .Where(m => m.IdMateria == idMateria && m.Estado == "APROBADO")
                .ToListAsync();
        }
    }
}