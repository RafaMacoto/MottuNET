using MottuNET.DTOs.Moto;
using MottuNET.Models;

namespace MottuNET.Services.Interfaces
{
    public interface IMotoService
    {
        Task<MotoResponseDTO?> GetByIdAsync(int id);
        Task<IEnumerable<MotoResponseDTO>> GetAllAsync(string? modelo = null, StatusMoto? status = null, int? alaId = null);
        Task<MotoResponseDTO> CreateAsync(MotoRequestDTO dto);
        Task<MotoResponseDTO?> UpdateAsync(int id, MotoRequestDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
