using MottuNET.data;
using MottuNET.DTOs.Ala;
using MottuNET.DTOs.Moto;
using MottuNET.Models;
using MottuNET.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MottuNET.Services
{
    public class AlaService : IAlaService
    {
        private readonly AppDbContext _context;

        public AlaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AlaResponseDTO>> GetAllAsync()
        {
            var alas = await _context.Alas.Include(a => a.Motos).ToListAsync();
            return alas.Select(a => MapToResponse(a));
        }

        public async Task<AlaResponseDTO?> GetByIdAsync(int id)
        {
            var ala = await _context.Alas.Include(a => a.Motos)
                                         .FirstOrDefaultAsync(a => a.Id == id);
            return ala == null ? null : MapToResponse(ala);
        }

        public async Task<AlaResponseDTO> CreateAsync(AlaRequestDTO dto)
        {
            var ala = new Ala { Nome = dto.Nome, Motos = new List<Moto>() };
            _context.Alas.Add(ala);
            await _context.SaveChangesAsync();
            return MapToResponse(ala);
        }

        public async Task<AlaResponseDTO?> UpdateAsync(int id, AlaRequestDTO dto)
        {
            var ala = await _context.Alas.FindAsync(id);
            if (ala == null) return null;

            ala.Nome = dto.Nome;
            _context.Alas.Update(ala);
            await _context.SaveChangesAsync();

            return MapToResponse(ala);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ala = await _context.Alas.FindAsync(id);
            if (ala == null) return false;

            _context.Alas.Remove(ala);
            await _context.SaveChangesAsync();
            return true;
        }

        private static AlaResponseDTO MapToResponse(Ala ala)
        {
            return new AlaResponseDTO(
                ala.Id,
                ala.Nome,
                ala.Motos.Select(m => new MotoResponseDTO(
                    m.Id,
                    m.Modelo,
                    m.Status,
                    m.Posicao,
                    m.Problema,
                    m.Placa
                ))
            );
        }
    }
}
