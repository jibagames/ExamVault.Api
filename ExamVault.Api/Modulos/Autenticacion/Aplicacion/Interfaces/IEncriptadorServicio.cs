namespace ExamVault.Api.Modulos.Autenticacion.Aplicacion.Interfaces
{
    public interface IEncriptadorServicio
    {
        string Encriptar(string textoPlano);
        bool Verificar(string textoPlano, string hash);
    }
}