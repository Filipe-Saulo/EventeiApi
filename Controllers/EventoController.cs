using Api.IRepository;
using Api.Models.Data;
using Api.Models.Dto;
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

            return Ok(eventos);
        }

        [HttpPost]
        public async Task<ActionResult<EventoDto>> CreateEvento([FromBody] CreateEventoDto createEventoDto)
        {
            if (createEventoDto == null)
            {
                return BadRequest("Os dados do evento são inválidos.");
            }

            // Chama o repositório para adicionar o evento com as fotos
            var eventoSalvo = await _eventoRepository.AddEventoComFotosAsync(createEventoDto);

            // Retorna o evento salvo com status Created
            return CreatedAtAction(nameof(GetAllEventos), new { id = eventoSalvo.EventoId }, eventoSalvo);
        }

    }
}
