using MottuNET.DTOs.Ala;
using Swashbuckle.AspNetCore.Filters;

namespace MottuNET.SwaggerExamples
{
    public class AlaRequestExample : IExamplesProvider<AlaRequestDTO>
    {
        public AlaRequestDTO GetExamples()
        {
            return new AlaRequestDTO(
                "Ala de Teste"
            );
        }
    }
}
