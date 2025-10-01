using MottuNET.DTOs.Usuario;
using MottuNET.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using MottuNET.SwaggerExamples;

namespace MottuNET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os usuários", Description = "Retorna todos os usuários cadastrados no sistema")]
        [SwaggerResponse(200, "Lista de usuários retornada com sucesso", typeof(IEnumerable<UsuarioResponseDTO>))]
        public async Task<ActionResult<IEnumerable<UsuarioResponseDTO>>> GetAll()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return Ok(usuarios);
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
