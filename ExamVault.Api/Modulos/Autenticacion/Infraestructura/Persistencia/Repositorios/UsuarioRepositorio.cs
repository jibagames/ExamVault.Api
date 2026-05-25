using ExamVault.Api.Infraestructura.Datos;
using ExamVault.Api.Modulos.Autenticacion.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Autenticacion.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ExamVault.Api.Modulos.Autenticacion.Infraestructura.Persistencia.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _contexto;

        public UsuarioRepositorio(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> ExisteCorreoAsync(string correo)
        {
            return await _contexto.Usuarios.AnyAsync(u => u.Correo == correo);
        }

        public async Task<Usuario?> ObtenerPorCorreoAsync(string correo)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
        }

        public async Task<Usuario?> ObtenerPorIdAsync(int idUsuario)
        {
            return await _contexto.Usuarios.FindAsync(idUsuario);
        }

        public async Task AgregarAsync(Usuario usuario)
        {
            await _contexto.Usuarios.AddAsync(usuario);
            await _contexto.SaveChangesAsync();
        }

        public async Task AgregarRolAsync(UsuarioRol usuarioRol)
        {
            await _contexto.UsuariosRoles.AddAsync(usuarioRol);
            await _contexto.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
            await _contexto.SaveChangesAsync();
        }

        public async Task<IList<string>> ObtenerRolesDeUsuarioAsync(int idUsuario)
        {
            return await _contexto.UsuariosRoles
                .Where(ur => ur.IdUsuario == idUsuario)
                .Join(_contexto.Roles, ur => ur.IdRol, r => r.IdRol, (ur, r) => r.NombreRol)
                .ToListAsync();
        }
    }
}
