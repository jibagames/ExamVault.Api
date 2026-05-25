using ExamVault.Api.Modulos.Autenticacion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Autenticacion.Aplicacion.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<bool> ExisteCorreoAsync(string correo);
        Task<Usuario?> ObtenerPorCorreoAsync(string correo);
        Task<Usuario?> ObtenerPorIdAsync(int idUsuario);
        Task AgregarAsync(Usuario usuario);
        Task AgregarRolAsync(UsuarioRol usuarioRol);
        Task ActualizarAsync(Usuario usuario);
        Task<IList<string>> ObtenerRolesDeUsuarioAsync(int idUsuario);
    }
}