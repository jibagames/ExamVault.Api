using ExamVault.Api.Infraestructura.Datos;
using ExamVault.Api.Modulos.Repositorio.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Repositorio.Dominio.Entidades;
using ExamVault.API.Modulos.Repositorio.Dominio.Enums;
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

        public async Task<Material> AgregarAsync(Material material)
        {
            await _contexto.Materiales.AddAsync(material);
            await _contexto.SaveChangesAsync();
            return material;
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
                .AsNoTracking()
                .Where(m => m.IdMateria == idMateria && m.Estado == EstadoMaterial.Aprobado)
                .ToListAsync();
        }

        public async Task<int> ObtenerIdInstitucionPorMateriaAsync(int idMateria)
        {
            var materia = await _contexto.Materias
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdMateria == idMateria);

            return materia?.IdInstituciones ?? 0;
        }

        public async Task<long> CalcularAlmacenamientoUsadoAsync(int idInstitucion)
        {
            return await (from m in _contexto.Materiales.AsNoTracking()
                          join mat in _contexto.Materias.AsNoTracking() on m.IdMateria equals mat.IdMateria
                          where mat.IdInstituciones == idInstitucion
                          select (long)m.TamanoBytes).SumAsync();
        }
    }
}