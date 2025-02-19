using Api.IRepository;
using Api.Models.Data;
using Api.Models.Dto;
using AutoMapper;
using Eventei_Api.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class EventoRepository : IEventoRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public EventoRepository(DatabaseContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<List<Evento>> GetAllAsync()
        {
            return await _context.Eventos
                .Include(e => e.Photos)
                .Include(e => e.Comentarios)
                .ToListAsync();
        }

        public async Task<Evento> AddEventoComFotosAsync(CreateEventoDto createEventoDto)
        {
            
            var evento = _mapper.Map<Evento>(createEventoDto);

            
            foreach (var photoDto in createEventoDto.Photos)
            {
                var photo = new Photo
                {
                    UrlPhoto = photoDto.UrlPhoto,
                    PosEvento = photoDto.PosEvento,
                    EventoId = evento.EventoId 
                };
                evento.Photos.Add(photo);
            }

            
            await _context.Eventos.AddAsync(evento);
            await _context.SaveChangesAsync();

            return evento; 
        }
    }
}
