using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MottuNET.data;
using MottuNET.model;
using MottuNET.Models;

namespace MottuNET.Controllers



{

    [Route("api/[controller]")]
    [ApiController]
    public class MotosController : ControllerBase
    {

        private readonly AppDbContext _context;

        public MotosController(AppDbContext context)
        {
            _context = context;
        }

      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moto>>> GetMotos([FromQuery] string modelo, [FromQuery] StatusMoto? status, [FromQuery] int? alaId)
        {
            var query = _context.Motos.AsQueryable();

            if (!string.IsNullOrEmpty(modelo))
                query = query.Where(m => m.Modelo.Contains(modelo));

            if (status.HasValue)
                query = query.Where(m => m.Status == status.Value);

            if (alaId.HasValue)
                query = query.Where(m => m.AlaId == alaId.Value);

            var motos = await query.Include(m => m.Ala).ToListAsync();

            if (motos.Count == 0)
                return NoContent();

            return motos;
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Moto>> GetMoto(int id)
        {
            var moto = await _context.Motos.Include(m => m.Ala).FirstOrDefaultAsync(m => m.Id == id);

            if (moto == null)
                return NotFound();

            return moto;
        }

        
        [HttpPost]
        public async Task<ActionResult<Moto>> PostMoto(Moto moto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMoto), new { id = moto.Id }, moto);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoto(int id, Moto moto)
        {
            if (id != moto.Id)
                return BadRequest();

            _context.Entry(moto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotoExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoto(int id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null)
                return NotFound();

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MotoExists(int id) =>
            _context.Motos.AnyAsync(m => m.Id == id).Result;
    }
}
