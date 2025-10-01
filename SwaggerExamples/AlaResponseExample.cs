using MottuNET.DTOs.Ala;
using MottuNET.DTOs.Moto;
using Swashbuckle.AspNetCore.Filters;

namespace MottuNET.SwaggerExamples
{
    public class AlaResponseExample : IExamplesProvider<AlaResponseDTO>
    {

        public AlaResponseDTO GetExamples()
        {
            return new AlaResponseDTO(
                1,
                "Ala de Teste",
                new List<MotoResponseDTO>
                {
                    new MotoResponseDTO(1, "Yamaha", Models.StatusMoto.DISPONIVEL, "A12", "Motor", "YZF-R3"),
                    new MotoResponseDTO(2, "Yamaha", Models.StatusMoto.DISPONIVEL, "A13", "Darol", "YZF-R4")
                }
            );
        }
    }
}
