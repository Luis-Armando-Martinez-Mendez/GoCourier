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
    public class NotificacionController : ControllerBase
    {
        private readonly GoCourierContext _context;

        public NotificacionController(GoCourierContext context)
        {
            _context = context;
        }

        // GET: api/notificacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notificacion>>> GetNotificaciones()
        {
            var notificaciones = await _context.Notificaciones.Include(n => n.Usuario).ToListAsync();
            return Ok(notificaciones);
        }

        // GET: api/notificacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notificacion>> GetNotificacion(int id)
        {
            var notificacion = await _context.Notificaciones.Include(n => n.Usuario).FirstOrDefaultAsync(m => m.Id == id);

            if (notificacion == null)
            {
                return NotFound();
            }

            return Ok(notificacion);
        }

        // POST: api/notificacion
        [HttpPost]
        public async Task<ActionResult<Notificacion>> CreateNotificacion([FromBody] Notificacion notificacion)
        {
            _context.Notificaciones.Add(notificacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNotificacion), new { id = notificacion.Id }, notificacion);
        }

        // PUT: api/notificacion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotificacion(int id, [FromBody] Notificacion notificacion)
        {
            if (id != notificacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(notificacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Notificaciones.Any(n => n.Id == id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/notificacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificacion(int id)
        {
            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion == null)
            {
                return NotFound();
            }

            _context.Notificaciones.Remove(notificacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
