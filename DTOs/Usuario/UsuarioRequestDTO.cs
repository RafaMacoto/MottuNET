namespace MottuNET.DTOs.Usuario
{
    public record UsuarioRequestDTO(
        string Nome,
        string Email,
        string Senha
    );
}
