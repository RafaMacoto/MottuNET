using MottuNET.DTOs.Usuario;
using MottuNET.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using MottuNET.SwaggerExamples;
using MottuNET.DTOs.Commons;

namespace MottuNET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private const int MaxPageSize = 100;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os usuários (paginado)", Description = "Retorna usuários cadastrados - suportando paginação e links HATEOAS")]
        [SwaggerResponse(200, "Lista de usuários retornada com sucesso", typeof(PagedResponse<UsuarioResponseDTO>))]
        public async Task<ActionResult<PagedResponse<UsuarioResponseDTO>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;
            pageSize = Math.Min(pageSize, MaxPageSize);

            var all = (await _usuarioService.GetAllAsync()).ToList();
            var total = all.Count;

            if (total == 0)
            {
                var empty = new PagedResponse<UsuarioResponseDTO>(Enumerable.Empty<UsuarioResponseDTO>(), 0, pageNumber, pageSize);
                empty.Links.Add(new LinkDTO("self", Url.Action(nameof(GetAll), "Usuario", new { pageNumber, pageSize }, Request.Scheme)!, "GET"));
                return Ok(empty);
            }

            var paged = all.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            
            var response = new PagedResponse<UsuarioResponseDTO>(paged, total, pageNumber, pageSize);

            
            response.Links.Add(new LinkDTO("self", Url.Action(nameof(GetAll), "Usuario", new { pageNumber, pageSize }, Request.Scheme)!, "GET"));
            if (pageNumber > 1)
                response.Links.Add(new LinkDTO("prev", Url.Action(nameof(GetAll), "Usuario", new { pageNumber = pageNumber - 1, pageSize }, Request.Scheme)!, "GET"));
            if (pageNumber < response.TotalPages)
                response.Links.Add(new LinkDTO("next", Url.Action(nameof(GetAll), "Usuario", new { pageNumber = pageNumber + 1, pageSize }, Request.Scheme)!, "GET"));

            return Ok(response);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca usuário por ID", Description = "Retorna um usuário específico pelo seu ID")]
        [SwaggerResponse(200, "Usuário encontrado", typeof(UsuarioResponseDTO))]
        [SwaggerResponse(404, "Usuário não encontrado")]
        public async Task<ActionResult<UsuarioResponseDTO>> GetById(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo usuário", Description = "Cadastra um novo usuário no sistema")]
        [SwaggerResponse(201, "Usuário criado com sucesso", typeof(UsuarioResponseDTO))]
        [SwaggerRequestExample(typeof(UsuarioRequestDTO), typeof(UsuarioRequestExample))]
        [SwaggerResponseExample(201, typeof(UsuarioResponseExample))]
        public async Task<ActionResult<UsuarioResponseDTO>> Create([FromBody] UsuarioRequestDTO dto)
        {
            var usuario = await _usuarioService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza um usuário", Description = "Atualiza os dados de um usuário existente pelo ID")]
        [SwaggerResponse(200, "Usuário atualizado com sucesso", typeof(UsuarioResponseDTO))]
        [SwaggerResponse(404, "Usuário não encontrado")]
        [SwaggerRequestExample(typeof(UsuarioRequestDTO), typeof(UsuarioRequestExample))]
        public async Task<ActionResult<UsuarioResponseDTO>> Update(int id, [FromBody] UsuarioRequestDTO dto)
        {
            var usuario = await _usuarioService.UpdateAsync(id, dto);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleta um usuário", Description = "Remove um usuário do sistema pelo ID")]
        [SwaggerResponse(204, "Usuário deletado com sucesso")]
        [SwaggerResponse(404, "Usuário não encontrado")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _usuarioService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
