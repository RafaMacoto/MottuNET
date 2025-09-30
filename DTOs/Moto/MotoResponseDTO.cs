using MottuNET.Models;
using MottuNET.DTOs.Ala;

namespace MottuNET.DTOs.Moto
{
    public record MotoResponseDTO(
        int Id,
        string Modelo,
        StatusMoto Status,
        string Posicao,
        string? Problema,
        string? Placa
    );
}
