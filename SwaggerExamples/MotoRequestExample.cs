using MottuNET.DTOs.Moto;
using MottuNET.Models;
using Swashbuckle.AspNetCore.Filters;

namespace MottuNET.SwaggerExamples
{
    public class MotoRequestExample : IExamplesProvider<MotoRequestDTO>
    {

        public MotoRequestDTO GetExamples()
        {
            return new MotoRequestDTO(
                Modelo: "Honda CG 160",
                Status: StatusMoto.MANUTENCAO,
                Posicao: "Garagem A1",
                Problema: "Sem problemas",
                Placa: "ABC1234",
                AlaId: 1
            );
        }
    }
}
