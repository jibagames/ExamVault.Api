using ExamVault.API.Modulos.Repositorio.Dominio.Enums;
using System.ComponentModel.DataAnnotations;

namespace ExamVault.Api.Modulos.Repositorio.Aplicacion.DTOs
{
    public class ModerarMaterialDto
    {
        [Required(ErrorMessage = "El estado es obligatorio.")]
        [RegularExpression("^(APROBADO|RECHAZADO)$", ErrorMessage = "El estado solo puede ser APROBADO o RECHAZADO.")]
        public EstadoMaterial NuevoEstado { get; set; }
    }
}