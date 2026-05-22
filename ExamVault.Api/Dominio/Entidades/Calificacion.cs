namespace ExamVault.Api.Dominio.Entidades
{
    public class Calificacion
    {
        public int IdCalificacion { get; set; }
        public int Estrellas { get; set; }
        public string? Comentario { get; set; }
        public DateTime CreadoEn { get; set; }
        public int IdSesion { get; set; }
    }
}

