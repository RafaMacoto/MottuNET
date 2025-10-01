using Microsoft.AspNetCore.Mvc;
using MottuNET.DTOs.Ala;
using MottuNET.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using MottuNET.SwaggerExamples;
using MottuNET.DTOs.Commons;

namespace MottuNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlasController : ControllerBase
    {
        private readonly IAlaService _alaService;
        private const int MaxPageSize = 100;

        public AlasController(IAlaService alaService)
        {
            _alaService = alaService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todas as alas (paginado)", Description = "Retorna alas com paginação e HATEOAS")]
        [SwaggerResponse(200, "Lista de alas retornada com sucesso", typeof(PagedResponse<AlaResponseDTO>))]
        [SwaggerResponse(204, "Nenhuma ala encontrada")]
        public async Task<ActionResult<PagedResponse<AlaResponseDTO>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;
            pageSize = Math.Min(pageSize, MaxPageSize);

            var all = (await _alaService.GetAllAsync()).ToList();
            var total = all.Count;

            if (total == 0)
            {
                var empty = new PagedResponse<AlaResponseDTO>(Enumerable.Empty<AlaResponseDTO>(), 0, pageNumber, pageSize);
                empty.Links.Add(new LinkDTO("self", Url.Action(nameof(GetAll), "Alas", new { pageNumber, pageSize }, Request.Scheme)!, "GET"));
                return Ok(empty);
            }

            var paged = all.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var response = new PagedResponse<AlaResponseDTO>(paged, total, pageNumber, pageSize);

            response.Links.Add(new LinkDTO("self", Url.Action(nameof(GetAll), "Alas", new { pageNumber, pageSize }, Request.Scheme)!, "GET"));
            if (pageNumber > 1)
                response.Links.Add(new LinkDTO("prev", Url.Action(nameof(GetAll), "Alas", new { pageNumber = pageNumber - 1, pageSize }, Request.Scheme)!, "GET"));
            if (pageNumber < response.TotalPages)
                response.Links.Add(new LinkDTO("next", Url.Action(nameof(GetAll), "Alas", new { pageNumber = pageNumber + 1, pageSize }, Request.Scheme)!, "GET"));

            return Ok(response);
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
