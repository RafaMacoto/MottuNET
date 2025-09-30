using Microsoft.AspNetCore.Mvc;
using MottuNET.DTOs.Moto;
using MottuNET.Models;
using MottuNET.Services.Interfaces;

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
        public async Task<ActionResult<MotoResponseDTO>> GetById(int id)
        {
            var moto = await _motoService.GetByIdAsync(id);
            if (moto == null) return NotFound();
            return Ok(moto);
        }

        
        [HttpPost]
        public async Task<ActionResult<MotoResponseDTO>> Create(MotoRequestDTO dto)
        {
            var moto = await _motoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = moto.Id }, moto);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MotoRequestDTO dto)
        {
            var moto = await _motoService.UpdateAsync(id, dto);
            if (moto == null) return NotFound();
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _motoService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
