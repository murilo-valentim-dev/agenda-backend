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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluguel>>> GetAlugueis()
        {
            return await _context.Alugueis.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Aluguel>> PostAluguel(Aluguel aluguel)
        {
            aluguel.Data = DateTime.SpecifyKind(aluguel.Data, DateTimeKind.Utc);

            _context.Alugueis.Add(aluguel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlugueis), new { id = aluguel.Id }, aluguel);
        }

        [HttpGet("por-data/{data}")]
        public async Task<ActionResult<IEnumerable<Aluguel>>> GetAlugueisPorData(string data)
        {
            if (!DateTime.TryParse(data, out DateTime dataConvertida))
            {
                return BadRequest("Data inválida");
            }

            dataConvertida = DateTime.SpecifyKind(dataConvertida, DateTimeKind.Utc);

            var alugueis = await _context.Alugueis
                .Where(a => a.Data.Date == dataConvertida.Date)
                .ToListAsync();

            return alugueis;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluguel(int id, Aluguel aluguel)
        {
            if (id != aluguel.Id)
            {
                return BadRequest("ID do aluguel não confere.");
            }

            aluguel.Data = DateTime.SpecifyKind(aluguel.Data, DateTimeKind.Utc);

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
