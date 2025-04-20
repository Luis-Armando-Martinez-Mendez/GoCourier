using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoCourier.Application.Interfaces;
using GoCourier.Domain.Entities;
using GoCourier.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GoCourier.Application.Service
{
    public class EnvioService : IEnvioService
    {
        private readonly GoCourierContext _context;

        public EnvioService(GoCourierContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Envio>> GetAllEnvios() => await _context.Envios.Include(e => e.Usuario).ToListAsync();
        public async Task<Envio> GetEnvioById(int id) => await _context.Envios.Include(e => e.Usuario).FirstOrDefaultAsync(e => e.Id == id);

        public async Task<Envio> CreateEnvio(Envio envio)
        {
            _context.Envios.Add(envio);
            await _context.SaveChangesAsync();
            return envio;
        }

        public async Task<bool> UpdateEnvio(Envio envio)
        {
            _context.Entry(envio).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEnvio(int id)
        {
            var envio = await _context.Envios.FindAsync(id);
            if (envio == null) return false;

            _context.Envios.Remove(envio);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}