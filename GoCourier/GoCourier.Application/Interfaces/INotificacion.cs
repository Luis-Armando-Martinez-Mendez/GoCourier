using GoCourier.Domain.Entities;
namespace GoCourier.Application.Interfaces
{
    public interface INotificacionService
    {
        Task<IEnumerable<Notificacion>> GetAllNotificaciones();
        Task<Notificacion> GetNotificacionById(int id);
        Task<Notificacion> CreateNotificacion(Notificacion notificacion);
        Task<bool> UpdateNotificacion(Notificacion notificacion);
        Task<bool> DeleteNotificacion(int id);
    }
}
