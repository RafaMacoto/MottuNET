using MottuNET.DTOs.Usuario;
using Swashbuckle.AspNetCore.Filters;

namespace MottuNET.SwaggerExamples
{
    public class UsuarioRequestExample : IExamplesProvider<UsuarioRequestDTO>
    {

        public UsuarioRequestDTO GetExamples()
        {
            return new UsuarioRequestDTO(
                "Rafael Macoto",
                "rafael@example.com",
                "senha123"
            );
        }

    }
}
