using Api.Models.Data;
using Api.Models.Dto;

namespace Api.IRepository
{
    public interface IEventoRepository
    {
        Task<List<Evento>> GetAllAsync();
        Task<Evento> AddEventoComFotosAsync(CreateEventoDto eventoCreateDto);
    }
}
