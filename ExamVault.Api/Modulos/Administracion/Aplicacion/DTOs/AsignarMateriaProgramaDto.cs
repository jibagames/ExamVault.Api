using System.ComponentModel.DataAnnotations;

namespace ExamVault.Api.Modulos.Administracion.Aplicacion.DTOs
{
    public class AsignarMateriaProgramaDto
    {
        [Required]
        public int IdPrograma { get; set; }

        [Required]
        public int IdMateria { get; set; }
    }
}
