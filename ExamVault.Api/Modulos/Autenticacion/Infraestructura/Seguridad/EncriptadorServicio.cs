using ExamVault.Api.Modulos.Autenticacion.Aplicacion.Interfaces;

namespace ExamVault.Api.Modulos.Autenticacion.Infraestructura.Seguridad
{
    public class EncriptadorServicio : IEncriptadorServicio
    {
        public string Encriptar(string textoPlano)
        {
            return BCrypt.Net.BCrypt.HashPassword(textoPlano);
        }

        public bool Verificar(string textoPlano, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(textoPlano, hash);
        }
    }
}
