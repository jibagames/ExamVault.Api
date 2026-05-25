namespace ExamVault.Api.Modulos.Repositorio.Aplicacion.DTOs
{
    public class MaterialRespuestaDto
    {
        public int IdMaterial { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int TamanoBytes { get; set; }
        public int IdTipoMaterial { get; set; }
    }
}