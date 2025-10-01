using MottuNET.DTOs.Usuario;
using Swashbuckle.AspNetCore.Filters;

namespace MottuNET.SwaggerExamples
{
    public class UsuarioResponseExample : IExamplesProvider<UsuarioResponseDTO>
    {
        public UsuarioResponseDTO GetExamples()
        {
            return new UsuarioResponseDTO(
                1,
                "Rafael Macoto",
                "rafael@example.com"
            );
        }
    }
}
