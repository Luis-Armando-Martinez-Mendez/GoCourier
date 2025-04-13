using GoCourier.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoCourier.infrastructure.Context;
using System.Threading.Tasks;
using System.Linq;


public class BuscadorController : Controller
{
    private readonly GoCourierContext _context;

    public BuscadorController(GoCourierContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string email)
    {
        var query = _context.Envios.Include(e => e.Usuario).AsQueryable();

        if (!string.IsNullOrEmpty(email))
        {
            query = query.Where(e => e.Usuario!.Email!.Contains(email));
        }

        var resultados = await query.ToListAsync();

        return View(resultados); 
    }
}
