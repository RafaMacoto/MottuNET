using MottuNET.DTOs.Ala;

namespace MottuNET.Services.Interfaces
{
    public interface IAlaService
    {
        Task<AlaResponseDTO?> GetByIdAsync(int id);
        Task<IEnumerable<AlaResponseDTO>> GetAllAsync();
        Task<AlaResponseDTO> CreateAsync(AlaRequestDTO dto);
        Task<AlaResponseDTO?> UpdateAsync(int id, AlaRequestDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
