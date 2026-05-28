using ExamVault.API.Modulos.Repositorio.Dominio.Enums;

namespace ExamVault.Api.Modulos.Repositorio.Dominio.Entidades
{
    public class Material
    {
        public int IdMaterial { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public EstadoMaterial Estado { get; set; } = EstadoMaterial.Pendiente;
        public string UrlArchivo { get; set; } = string.Empty;
        public int TamanoBytes { get; set; }
        public int IdUsuario { get; set; }
        public int IdMateria { get; set; }
        public int? IdModerador { get; set; }
        public DateTime? FechaModeracion { get; set; }
        public int IdTipoMaterial { get; set; }
    }
}
