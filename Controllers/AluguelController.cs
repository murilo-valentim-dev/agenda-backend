using AluguelApi.Data;
using AluguelApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AluguelApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AluguelController : ControllerBase
    {
        private readonly DataContext _context;

        public AluguelController(DataContext context)
        {
            _context = context;
        }

        // GET: api/aluguel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluguel>>> GetAlugueis()
        {
            return await _context.Alugueis.ToListAsync();
        }

        // POST: api/aluguel
        [HttpPost]
        public async Task<ActionResult<Aluguel>> PostAluguel(Aluguel aluguel)
        {
            _context.Alugueis.Add(aluguel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlugueis), new { id = aluguel.Id }, aluguel);
        }

        // GET: api/aluguel/por-data/2024-06-10
        [HttpGet("por-data/{data}")]
        public async Task<ActionResult<IEnumerable<Aluguel>>> GetAlugueisPorData(string data)
        {
            if (!DateTime.TryParse(data, out DateTime dataConvertida))
            {
                return BadRequest("Data inválida");
            }

            var alugueis = await _context.Alugueis
                .Where(a => a.Data.Date == dataConvertida.Date)
                .ToListAsync();

            return alugueis;
        }

        // PUT: api/aluguel/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluguel(int id, Aluguel aluguel)
        {
            if (id != aluguel.Id)
            {
                return BadRequest("ID do aluguel não confere.");
            }

            _context.Entry(aluguel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AluguelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/aluguel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluguel(int id)
        {
            var aluguel = await _context.Alugueis.FindAsync(id);
            if (aluguel == null)
            {
                return NotFound();
            }

            _context.Alugueis.Remove(aluguel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AluguelExists(int id)
        {
            return _context.Alugueis.Any(e => e.Id == id);
        }
    }
}
