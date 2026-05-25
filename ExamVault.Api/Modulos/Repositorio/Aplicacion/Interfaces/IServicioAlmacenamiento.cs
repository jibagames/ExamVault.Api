namespace ExamVault.Api.Modulos.Repositorio.Aplicacion.Interfaces
{
    public interface IServicioAlmacenamiento
    {
        Task<string> SubirArchivoAsync(Stream flujoArchivo, string nombreArchivo, string tipoContenido);
        Task<bool> EliminarArchivoAsync(string llaveObjeto);
        string GenerarUrlPreFirmada(string llaveObjeto, int minutosExpiracion = 15);
    }
}