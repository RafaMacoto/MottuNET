using MottuNET.DTOs.Usuario;

namespace MottuNET.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioResponseDTO>> GetAllAsync();
        Task<UsuarioResponseDTO?> GetByIdAsync(int id);
        Task<UsuarioResponseDTO> CreateAsync(UsuarioRequestDTO request);
        Task<UsuarioResponseDTO?> UpdateAsync(int id, UsuarioRequestDTO request);
        Task<bool> DeleteAsync(int id);
    }
}
