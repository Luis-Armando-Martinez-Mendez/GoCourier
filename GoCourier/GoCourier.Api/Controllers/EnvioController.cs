using Microsoft.AspNetCore.Mvc;
using GoCourier.Api.Dtos;
using GoCourier.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using GoCourier.infrastructure.Context;

namespace GoCourier.Api.Controllers
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

        // GET: api/Envio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnvioDto>>> GetEnvios()
        {
            var envios = await _context.Envios
                .Include(e => e.Usuario)
                .Select(e => new EnvioDto
                {
                    Id = e.Id,
                    UsuarioId = e.UsuarioId,
                    Direccion = e.Direccion,
                    Descripcion = e.Descripcion,
                    Estado = e.Estado,
                    Fecha = e.Fecha,
                    Usuario = e.Usuario != null ? new UsuarioDto
                    {
                        Id = e.Usuario.Id,
                        Nombre = e.Usuario.Nombre,
                        Email = e.Usuario.Email
                    } : null
                })
                .ToListAsync();

            return Ok(envios);
        }

        // GET: api/Envio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EnvioDto>> GetEnvio(int id)
        {
            var envio = await _context.Envios
                .Include(e => e.Usuario)
                .Where(e => e.Id == id)
                .Select(e => new EnvioDto
                {
                    Id = e.Id,
                    UsuarioId = e.UsuarioId,
                    Direccion = e.Direccion,
                    Descripcion = e.Descripcion,
                    Estado = e.Estado,
                    Fecha = e.Fecha,
                    Usuario = e.Usuario != null ? new UsuarioDto
                    {
                        Id = e.Usuario.Id,
                        Nombre = e.Usuario.Nombre,
                        Email = e.Usuario.Email
                    } : null
                })
                .FirstOrDefaultAsync();

            if (envio == null)
                return NotFound();

            return Ok(envio);
        }

        // POST: api/Envio
        [HttpPost]
        public async Task<ActionResult<EnvioDto>> CreateEnvio(EnvioDto dto)
        {
            var envio = new Envio
            {
                UsuarioId = dto.UsuarioId,
                Direccion = dto.Direccion,
                Descripcion = dto.Descripcion,
                Estado = dto.Estado,
                Fecha = dto.Fecha
            };

            _context.Envios.Add(envio);
            await _context.SaveChangesAsync();

            dto.Id = envio.Id;

            return CreatedAtAction(nameof(GetEnvio), new { id = dto.Id }, dto);
        }

        // PUT: api/Envio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnvio(int id, EnvioDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var envio = await _context.Envios.FindAsync(id);
            if (envio == null)
                return NotFound();

            envio.Direccion = dto.Direccion;
            envio.Descripcion = dto.Descripcion;
            envio.Estado = dto.Estado;
            envio.Fecha = dto.Fecha;

            _context.Entry(envio).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Envio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnvio(int id)
        {
            var envio = await _context.Envios.FindAsync(id);
            if (envio == null)
                return NotFound();

            _context.Envios.Remove(envio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}