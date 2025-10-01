using Microsoft.AspNetCore.Mvc;
using MottuNET.DTOs.Ala;
using MottuNET.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using MottuNET.SwaggerExamples;

namespace MottuNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlasController : ControllerBase
    {
        private readonly IAlaService _alaService;

        public AlasController(IAlaService alaService)
        {
            _alaService = alaService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todas as alas", Description = "Retorna todas as alas cadastradas no sistema")]
        [SwaggerResponse(200, "Lista de alas retornada com sucesso", typeof(IEnumerable<AlaResponseDTO>))]
        [SwaggerResponse(204, "Nenhuma ala encontrada")]
        public async Task<ActionResult<IEnumerable<AlaResponseDTO>>> GetAll()
        {
            var alas = await _alaService.GetAllAsync();
            if (!alas.Any()) return NoContent();
            return Ok(alas);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca ala por ID", Description = "Retorna uma ala específica pelo ID")]
        [SwaggerResponse(200, "Ala encontrada", typeof(AlaResponseDTO))]
        [SwaggerResponse(404, "Ala não encontrada")]
        public async Task<ActionResult<AlaResponseDTO>> GetById(int id)
        {
            var ala = await _alaService.GetByIdAsync(id);
            if (ala == null) return NotFound();
            return Ok(ala);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova ala", Description = "Cadastra uma nova ala no sistema")]
        [SwaggerResponse(201, "Ala criada com sucesso", typeof(AlaResponseDTO))]
        [SwaggerRequestExample(typeof(AlaRequestDTO), typeof(AlaRequestExample))]
        [SwaggerResponseExample(201, typeof(AlaResponseExample))]
        public async Task<ActionResult<AlaResponseDTO>> Create(AlaRequestDTO dto)
        {
            var ala = await _alaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = ala.Id }, ala);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza uma ala", Description = "Atualiza os dados de uma ala existente pelo ID")]
        [SwaggerResponse(204, "Ala atualizada com sucesso")]
        [SwaggerResponse(404, "Ala não encontrada")]
        [SwaggerRequestExample(typeof(AlaRequestDTO), typeof(AlaRequestExample))]
        public async Task<IActionResult> Update(int id, AlaRequestDTO dto)
        {
            var ala = await _alaService.UpdateAsync(id, dto);
            if (ala == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleta uma ala", Description = "Remove uma ala do sistema pelo ID")]
        [SwaggerResponse(204, "Ala deletada com sucesso")]
        [SwaggerResponse(404, "Ala não encontrada")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _alaService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
