using Microsoft.AspNetCore.Mvc;
using MottuNET.DTOs.Moto;
using MottuNET.Models;
using MottuNET.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using MottuNET.SwaggerExamples;

namespace MottuNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotosController : ControllerBase
    {
        private readonly IMotoService _motoService;

        public MotosController(IMotoService motoService)
        {
            _motoService = motoService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todas as motos", Description = "Retorna todas as motos cadastradas, com filtros opcionais de modelo, status ou ala")]
        [SwaggerResponse(200, "Lista de motos retornada com sucesso", typeof(IEnumerable<MotoResponseDTO>))]
        [SwaggerResponse(204, "Nenhuma moto encontrada")]
        public async Task<ActionResult<IEnumerable<MotoResponseDTO>>> GetAll(
            [FromQuery] string? modelo,
            [FromQuery] StatusMoto? status,
            [FromQuery] int? alaId)
        {
            var motos = await _motoService.GetAllAsync(modelo, status, alaId);
            if (!motos.Any()) return NoContent();
            return Ok(motos);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca moto por ID", Description = "Retorna uma moto específica pelo seu ID")]
        [SwaggerResponse(200, "Moto encontrada", typeof(MotoResponseDTO))]
        [SwaggerResponse(404, "Moto não encontrada")]
        public async Task<ActionResult<MotoResponseDTO>> GetById(int id)
        {
            var moto = await _motoService.GetByIdAsync(id);
            if (moto == null) return NotFound();
            return Ok(moto);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova moto", Description = "Cadastra uma nova moto no sistema")]
        [SwaggerResponse(201, "Moto criada com sucesso", typeof(MotoResponseDTO))]
        [SwaggerRequestExample(typeof(MotoRequestDTO), typeof(MotoRequestExample))]
        [SwaggerResponseExample(201, typeof(MotoResponseExample))]
        public async Task<ActionResult<MotoResponseDTO>> Create(MotoRequestDTO dto)
        {
            var moto = await _motoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = moto.Id }, moto);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza uma moto", Description = "Atualiza os dados de uma moto existente pelo ID")]
        [SwaggerResponse(204, "Moto atualizada com sucesso")]
        [SwaggerResponse(404, "Moto não encontrada")]
        [SwaggerRequestExample(typeof(MotoRequestDTO), typeof(MotoRequestExample))]
        public async Task<IActionResult> Update(int id, MotoRequestDTO dto)
        {
            var moto = await _motoService.UpdateAsync(id, dto);
            if (moto == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleta uma moto", Description = "Remove uma moto do sistema pelo ID")]
        [SwaggerResponse(204, "Moto deletada com sucesso")]
        [SwaggerResponse(404, "Moto não encontrada")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _motoService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
