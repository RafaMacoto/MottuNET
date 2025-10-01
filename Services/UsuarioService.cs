using MottuNET.data;
using MottuNET.DTOs.Usuario;
using MottuNET.Services.Interfaces;
using MottuNET.Models;
using Microsoft.EntityFrameworkCore;

namespace MottuNET.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioResponseDTO>> GetAllAsync()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return usuarios.Select(u => MapToResponse(u));
        }

        public async Task<UsuarioResponseDTO?> GetByIdAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            return usuario == null ? null : MapToResponse(usuario);
        }

        public async Task<UsuarioResponseDTO> CreateAsync(UsuarioRequestDTO dto)
        {
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha 
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return MapToResponse(usuario);
        }

        public async Task<UsuarioResponseDTO?> UpdateAsync(int id, UsuarioRequestDTO dto)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return null;

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.Senha = dto.Senha;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return MapToResponse(usuario);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        private static UsuarioResponseDTO MapToResponse(Usuario usuario)
        {
            return new UsuarioResponseDTO(
                usuario.Id,
                usuario.Nome,
                usuario.Email
            );
        }
    }
}
