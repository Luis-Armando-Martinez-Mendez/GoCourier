using Microsoft.AspNetCore.Mvc;
using GoCourier.Api.Dtos;
using GoCourier.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using GoCourier.infrastructure.Context;

namespace GoCourier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly GoCourierContext _context;

        public UsuarioController(GoCourierContext context)
        {
            _context = context;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Email = u.Email,
                    Contraseña = u.Contraseña,
                    Direccion = u.Direccion,
                    Pais = u.Pais
                })
                .ToListAsync();

            return Ok(usuarios);
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.Id == id)
                .Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Email = u.Email,
                    Contraseña = u.Contraseña,
                    Direccion = u.Direccion,
                    Pais = u.Pais
                })
                .FirstOrDefaultAsync();

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        // POST: api/Usuario
        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> CreateUsuario(UsuarioDto dto)
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Contraseña = dto.Contraseña,
                Direccion = dto.Direccion,
                Pais = dto.Pais
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            dto.Id = usuario.Id;

            return CreatedAtAction(nameof(GetUsuario), new { id = dto.Id }, dto);
        }

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, UsuarioDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();

            usuario.Nombre = dto.Nombre;
            usuario.Email = dto.Email;
            usuario.Contraseña = dto.Contraseña;
            usuario.Direccion = dto.Direccion;
            usuario.Pais = dto.Pais;

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}