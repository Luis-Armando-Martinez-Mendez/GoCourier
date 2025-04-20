using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoCourier.Application.Interfaces;
using GoCourier.Domain.Entities;
using GoCourier.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GoCourier.Application.Service
{
    public class NotificacionService : INotificacionService
    {
        private readonly GoCourierContext _context;

        public NotificacionService(GoCourierContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notificacion>> GetAllNotificaciones() => await _context.Notificaciones.Include(n => n.Usuario).ToListAsync();
        public async Task<Notificacion> GetNotificacionById(int id) => await _context.Notificaciones.Include(n => n.Usuario).FirstOrDefaultAsync(n => n.Id == id);

        public async Task<Notificacion> CreateNotificacion(Notificacion notificacion)
        {
            _context.Notificaciones.Add(notificacion);
            await _context.SaveChangesAsync();
            return notificacion;
        }

        public async Task<bool> UpdateNotificacion(Notificacion notificacion)
        {
            _context.Entry(notificacion).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteNotificacion(int id)
        {
            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion == null) return false;

            _context.Notificaciones.Remove(notificacion);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}