using MottuNET.Models;

namespace MottuNET.DTOs.Moto
{
    public record MotoRequestDTO(
        string Modelo,
        StatusMoto Status,
        string Posicao,
        string? Problema,
        string? Placa,
        int AlaId
    );
}
