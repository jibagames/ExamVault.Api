using System.ComponentModel.DataAnnotations;

public class AsignarMateriaMonitorDto
{
    [Required]
    public int IdMonitor { get; set; }

    [Required]
    public int IdMateria { get; set; }
}