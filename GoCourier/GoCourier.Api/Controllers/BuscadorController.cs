using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoCourier.Api.Dtos;
using System.Linq;
using System.Threading.Tasks;
using GoCourier.infrastructure.Context;

namespace GoCourier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuscadorController : ControllerBase
    {
        private readonly GoCourierContext _context;

        public BuscadorController(GoCourierContext context)
        {
            _context = context;
        }

        // GET: api/Buscador?email=usuario@example.com
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnvioDto>>> BuscarEnvios(string email)
        {
            var query = _context.Envios.Include(e => e.Usuario).AsQueryable();

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(e => e.Usuario != null && e.Usuario.Email.Contains(email));
            }

            var resultados = await query
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

            return Ok(resultados);
        }
    }
}
