using Microsoft.AspNetCore.Mvc;
using GoCourier.Api.Dtos;
using GoCourier.infrastructure.Context;
using GoCourier.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoCourier.Api.Controllers
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

        // GET: api/Notificacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificacionDto>>> GetNotificaciones()
        {
            var notificaciones = await _context.Notificaciones
                .Include(n => n.Usuario)
                .Select(n => new NotificacionDto
                {
                    Id = n.Id,
                    UsuarioId = n.UsuarioId,
                    Mensaje = n.Mensaje,
                    Fecha = n.Fecha,
                    Usuario = n.Usuario != null ? new UsuarioDto
                    {
                        Id = n.Usuario.Id,
                        Nombre = n.Usuario.Nombre,
                        Email = n.Usuario.Email
                    } : null
                })
                .ToListAsync();

            return Ok(notificaciones);
        }

        // GET: api/Notificacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificacionDto>> GetNotificacion(int id)
        {
            var notificacion = await _context.Notificaciones
                .Include(n => n.Usuario)
                .Where(n => n.Id == id)
                .Select(n => new NotificacionDto
                {
                    Id = n.Id,
                    UsuarioId = n.UsuarioId,
                    Mensaje = n.Mensaje,
                    Fecha = n.Fecha,
                    Usuario = n.Usuario != null ? new UsuarioDto
                    {
                        Id = n.Usuario.Id,
                        Nombre = n.Usuario.Nombre,
                        Email = n.Usuario.Email
                    } : null
                })
                .FirstOrDefaultAsync();

            if (notificacion == null)
                return NotFound();

            return Ok(notificacion);
        }

        // POST: api/Notificacion
        [HttpPost]
        public async Task<ActionResult<NotificacionDto>> CreateNotificacion(NotificacionDto dto)
        {
            var notificacion = new Notificacion
            {
                UsuarioId = dto.UsuarioId,
                Mensaje = dto.Mensaje,
                Fecha = dto.Fecha
            };

            _context.Notificaciones.Add(notificacion);
            await _context.SaveChangesAsync();

            dto.Id = notificacion.Id;

            return CreatedAtAction(nameof(GetNotificacion), new { id = dto.Id }, dto);
        }

        // PUT: api/Notificacion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotificacion(int id, NotificacionDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion == null)
                return NotFound();

            notificacion.Mensaje = dto.Mensaje;
            notificacion.Fecha = dto.Fecha;

            _context.Entry(notificacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Notificacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificacion(int id)
        {
            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion == null)
                return NotFound();

            _context.Notificaciones.Remove(notificacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
