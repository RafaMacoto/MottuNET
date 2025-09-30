using MottuNET.DTOs.Moto;
namespace MottuNET.DTOs.Ala
{
    public record AlaResponseDTO(
        int Id,
        string Nome,
        IEnumerable<MotoResponseDTO> Motos
    );
}
