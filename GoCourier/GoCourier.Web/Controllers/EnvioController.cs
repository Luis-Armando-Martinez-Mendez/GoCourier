using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoCourier.Domain.Entities;
using GoCourier.infrastructure.Context;

namespace GoCourier.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvioController : ControllerBase
    {
        private readonly GoCourierContext _context;

        public EnvioController(GoCourierContext context)
        {
            _context = context;
        }

        // GET: api/envio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Envio>>> GetEnvios()
        {
            var envios = await _context.Envios.Include(e => e.Usuario).ToListAsync();
            return Ok(envios);
        }

        // GET: api/envio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Envio>> GetEnvio(int id)
        {
            var envio = await _context.Envios.Include(e => e.Usuario).FirstOrDefaultAsync(m => m.Id == id);

            if (envio == null)
            {
                return NotFound();
            }

            return Ok(envio);
        }

        // POST: api/envio
        [HttpPost]
        public async Task<ActionResult<Envio>> CreateEnvio([FromBody] Envio envio)
        {
            _context.Envios.Add(envio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEnvio), new { id = envio.Id }, envio);
        }

        // PUT: api/envio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnvio(int id, [FromBody] Envio envio)
        {
            if (id != envio.Id)
            {
                return BadRequest();
            }

            _context.Entry(envio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Envios.Any(e => e.Id == id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/envio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnvio(int id)
        {
            var envio = await _context.Envios.FindAsync(id);
            if (envio == null)
            {
                return NotFound();
            }

            _context.Envios.Remove(envio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}