using System.ComponentModel.DataAnnotations;

namespace ExamVault.Api.Modulos.Repositorio.DTOs
{
    public class SubirMaterialDto
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        [MaxLength(255, ErrorMessage = "El título no puede superar los 255 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La URL del archivo es obligatoria.")]
        [MaxLength(500, ErrorMessage = "La URL del archivo no puede superar los 500 caracteres.")]
        public string UrlArchivoR2 { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tamaño en bytes es obligatorio.")]
        public int TamañoBytes { get; set; }

        [Required(ErrorMessage = "El ID de la materia es obligatorio.")]
        public int IdMateria { get; set; }

        [Required(ErrorMessage = "El ID del tipo de material es obligatorio.")]
        public int IdTipoMaterial { get; set; }
    }
}