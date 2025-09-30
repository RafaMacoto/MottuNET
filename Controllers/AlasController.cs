using Microsoft.AspNetCore.Mvc;
using MottuNET.DTOs.Ala;
using MottuNET.Services.Interfaces;

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
        public async Task<ActionResult<IEnumerable<AlaResponseDTO>>> GetAll()
        {
            var alas = await _alaService.GetAllAsync();
            if (!alas.Any()) return NoContent();
            return Ok(alas);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<AlaResponseDTO>> GetById(int id)
        {
            var ala = await _alaService.GetByIdAsync(id);
            if (ala == null) return NotFound();
            return Ok(ala);
        }

        
        [HttpPost]
        public async Task<ActionResult<AlaResponseDTO>> Create(AlaRequestDTO dto)
        {
            var ala = await _alaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = ala.Id }, ala);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AlaRequestDTO dto)
        {
            var ala = await _alaService.UpdateAsync(id, dto);
            if (ala == null) return NotFound();
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _alaService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
