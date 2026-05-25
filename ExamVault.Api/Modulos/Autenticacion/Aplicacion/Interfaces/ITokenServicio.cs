using ExamVault.Api.Modulos.Autenticacion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Autenticacion.Aplicacion.Interfaces
{
    public interface ITokenServicio
    {
        string GenerarToken(Usuario usuario, IList<string> roles);
    }
}