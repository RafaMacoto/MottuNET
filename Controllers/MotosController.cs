using Microsoft.AspNetCore.Mvc;
using MottuNET.DTOs.Moto;
using MottuNET.Models;
using MottuNET.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using MottuNET.SwaggerExamples;
using MottuNET.DTOs.Commons;
using Microsoft.AspNetCore.Authorization;
using MottuNET.ML;

namespace MottuNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class MotosController : ControllerBase
    {
        private readonly IMotoService _motoService;
        private const int MaxPageSize = 100;

        public MotosController(IMotoService motoService)
        {
            _motoService = motoService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todas as motos (paginado)", Description = "Retorna motos com filtros opcionais e paginação + HATEOAS")]
        [SwaggerResponse(200, "Lista de motos retornada com sucesso", typeof(PagedResponse<MotoResponseDTO>))]
        [SwaggerResponse(204, "Nenhuma moto encontrada")]
        public async Task<ActionResult<PagedResponse<MotoResponseDTO>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? modelo = null,
            [FromQuery] StatusMoto? status = null,
            [FromQuery] int? alaId = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;
            pageSize = Math.Min(pageSize, MaxPageSize);

            var all = (await _motoService.GetAllAsync(modelo, status, alaId)).ToList();
            var total = all.Count;

            if (total == 0)
            {
                var empty = new PagedResponse<MotoResponseDTO>(Enumerable.Empty<MotoResponseDTO>(), 0, pageNumber, pageSize);
                empty.Links.Add(new LinkDTO("self", Url.Action(nameof(GetAll), "Motos", new { pageNumber, pageSize, modelo, status, alaId }, Request.Scheme)!, "GET"));
                return Ok(empty);
            }

            var paged = all.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var response = new PagedResponse<MotoResponseDTO>(paged, total, pageNumber, pageSize);

            response.Links.Add(new LinkDTO("self", Url.Action(nameof(GetAll), "Motos", new { pageNumber, pageSize, modelo, status, alaId }, Request.Scheme)!, "GET"));
            if (pageNumber > 1)
                response.Links.Add(new LinkDTO("prev", Url.Action(nameof(GetAll), "Motos", new { pageNumber = pageNumber - 1, pageSize, modelo, status, alaId }, Request.Scheme)!, "GET"));
            if (pageNumber < response.TotalPages)
                response.Links.Add(new LinkDTO("next", Url.Action(nameof(GetAll), "Motos", new { pageNumber = pageNumber + 1, pageSize, modelo, status, alaId }, Request.Scheme)!, "GET"));

            return Ok(response);
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


        [HttpPost("prever-ala")]
        public ActionResult<object> PreverAla([FromBody] MotoAlaData input)
        {
            var ml = new MotoAlaModel();
            var alaPrevista = ml.PreverAla(input.Problema, input.Status);

            return Ok(new
            {
                Problema = input.Problema,
                Status = input.Status,
                AlaPrevista = alaPrevista
            });
        }
    }
}
