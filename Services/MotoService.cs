using Microsoft.EntityFrameworkCore;
using MottuNET.data;
using MottuNET.DTOs;
using MottuNET.DTOs.Moto;
using MottuNET.Models;
using MottuNET.Services.Interfaces;

namespace MottuNET.Services
{
    public class MotoService : IMotoService
    {
        private readonly AppDbContext _context;

        public MotoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MotoResponseDTO>> GetAllAsync(string? modelo = null, StatusMoto? status = null, int? alaId = null)
        {
            var query = _context.Motos.Include(m => m.Ala).AsQueryable();

            if (!string.IsNullOrEmpty(modelo))
                query = query.Where(m => m.Modelo.Contains(modelo));

            if (status.HasValue)
                query = query.Where(m => m.Status == status.Value);

            if (alaId.HasValue)
                query = query.Where(m => m.AlaId == alaId.Value);

            var motos = await query.ToListAsync();
            return motos.Select(m => MapToResponse(m));
        }

        public async Task<MotoResponseDTO?> GetByIdAsync(int id)
        {
            var moto = await _context.Motos.Include(m => m.Ala).FirstOrDefaultAsync(m => m.Id == id);
            return moto == null ? null : MapToResponse(moto);
        }

        public async Task<MotoResponseDTO> CreateAsync(MotoRequestDTO dto)
        {
            var moto = new Moto
            {
                Modelo = dto.Modelo,
                Status = dto.Status,
                Posicao = dto.Posicao,
                Problema = dto.Problema,
                Placa = dto.Placa,
                AlaId = dto.AlaId
            };

            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();
            return MapToResponse(moto);
        }

        public async Task<MotoResponseDTO?> UpdateAsync(int id, MotoRequestDTO dto)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null) return null;

            moto.Modelo = dto.Modelo;
            moto.Status = dto.Status;
            moto.Posicao = dto.Posicao;
            moto.Problema = dto.Problema;
            moto.Placa = dto.Placa;
            moto.AlaId = dto.AlaId;

            _context.Motos.Update(moto);
            await _context.SaveChangesAsync();

            return MapToResponse(moto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null) return false;

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();
            return true;
        }

        private static MotoResponseDTO MapToResponse(Moto moto)
        {
            return new MotoResponseDTO(
                moto.Id,
                moto.Modelo,
                moto.Status,
                moto.Posicao,
                moto.Problema,
                moto.Placa
            );
        }
    }
}
