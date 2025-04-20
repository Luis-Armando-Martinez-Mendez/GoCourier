using GoCourier.Domain.Entities;

namespace GoCourier.Application.Interfaces
{
    public interface IEnvioService
    {
        Task<IEnumerable<Envio>> GetAllEnvios();
        Task<Envio> GetEnvioById(int id);
        Task<Envio> CreateEnvio(Envio envio);
        Task<bool> UpdateEnvio(Envio envio);
        Task<bool> DeleteEnvio(int id);
    }
}
