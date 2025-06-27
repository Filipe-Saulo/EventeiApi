using Api.Models.Data;
using Api.Models.Dto;
using Api.Repositories.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IMapper _mapper;

        public EventoController(IEventoRepository eventoRepository, IMapper mapper)
        {
            _eventoRepository = eventoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventos()
        {

            var eventos = await _eventoRepository.GetAllAsync();

            if (eventos == null || eventos.Count == 0)
            {
                return NotFound("Nenhum evento encontrado.");
            }

            var eventosDto = _mapper.Map<List<EventoDto>>(eventos);
            return Ok(eventosDto);
        }

        [HttpPost]
        public async Task<ActionResult<CreateEventoDto>> CreateEvento([FromBody] CreateEventoDto createEventoDto)
        {
            if (createEventoDto == null)
            {
                return BadRequest("Os dados do evento são inválidos.");
            }

            
            var eventoSalvo = await _eventoRepository.AddEventAsync(createEventoDto);

            var eventoDto = _mapper.Map<CreateEventoDto>(eventoSalvo);

            
            return CreatedAtAction(nameof(GetAllEventos), new { id = eventoSalvo.EventoId }, eventoDto);
        }

    }
}
