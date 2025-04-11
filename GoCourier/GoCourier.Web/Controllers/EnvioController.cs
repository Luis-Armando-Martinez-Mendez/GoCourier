using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoCourier.Domain.Entities;
using GoCourier.infrastructure.Context;

namespace GoCourier.Web.Controllers
{
    public class EnvioController : Controller
    {
        private readonly GoCourierContext _context;

        public EnvioController(GoCourierContext context)
        {
            _context = context;
        }

        // GET: Envio
        public async Task<IActionResult> Index()
        {
            var envios = await _context.Envios
                .Include(e => e.Usuario)
                .ToListAsync();
            return View(envios);
        }



        // GET: Envio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var envio = await _context.Envios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (envio == null)
            {
                return NotFound();
            }

            return View(envio);
        }

        // GET: Envio/Create
        public IActionResult Create()
        {
            ViewBag.Usuarios = new SelectList(_context.Usuarios, "Id", "Email");
            return View();
        }

        // POST: Envio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,Direccion,Descripcion,Estado,Fecha")] Envio envio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(envio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Usuarios = new SelectList(_context.Usuarios, "Id", "Email", envio.UsuarioId);
            return View(envio);
        }

        // GET: Envio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var envio = await _context.Envios.FindAsync(id);
            if (envio == null)
            {
                return NotFound();
            }

            ViewBag.Usuarios = new SelectList(_context.Usuarios, "Id", "Email", envio.UsuarioId);
            return View(envio);
        }

        // POST: Envio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,Direccion,Descripcion,Estado,Fecha")] Envio envio)
        {
            if (id != envio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(envio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnvioExists(envio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Usuarios = new SelectList(_context.Usuarios, "Id", "Email", envio.UsuarioId);
            return View(envio);
        }

        // GET: Envio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var envio = await _context.Envios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (envio == null)
            {
                return NotFound();
            }

            return View(envio);
        }

        // POST: Envio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var envio = await _context.Envios.FindAsync(id);
            if (envio != null)
            {
                _context.Envios.Remove(envio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnvioExists(int id)
        {
            return _context.Envios.Any(e => e.Id == id);
        }
    }
}