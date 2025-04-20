using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoCourier.Application.Interfaces;
using GoCourier.Domain.Entities;
using GoCourier.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GoCourier.Application.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly GoCourierContext _context;

        public UsuarioService(GoCourierContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuarios() => await _context.Usuarios.ToListAsync();
        public async Task<Usuario> GetUsuarioById(int id) => await _context.Usuarios.FindAsync(id);

        public async Task<Usuario> CreateUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> UpdateUsuario(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}