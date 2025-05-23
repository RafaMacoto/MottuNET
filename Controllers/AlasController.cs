using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MottuNET.data;
using MottuNET.Models;

namespace MottuNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlasController : ControllerBase
    {

        private readonly AppDbContext _context;

        public AlasController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ala>>> GetAlas()
        {
            return await _context.Alas.Include(a => a.Motos).ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Ala>> GetAla(int id)
        {
            var ala = await _context.Alas.Include(a => a.Motos)
                                         .FirstOrDefaultAsync(a => a.Id == id);

            if (ala == null)
                return NotFound();

            return ala;
        }

       
        [HttpPost]
        public async Task<ActionResult<Ala>> PostAla(Ala ala)
        {
            if (string.IsNullOrEmpty(ala.Nome))
                return BadRequest("Nome é obrigatório");

            _context.Alas.Add(ala);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAla), new { id = ala.Id }, ala);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAla(int id, Ala ala)
        {
            if (id != ala.Id)
                return BadRequest();

            _context.Entry(ala).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlaExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAla(int id)
        {
            var ala = await _context.Alas.FindAsync(id);
            if (ala == null)
                return NotFound();

            _context.Alas.Remove(ala);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlaExists(int id) =>
            _context.Alas.AnyAsync(a => a.Id == id).Result;
    }
}

