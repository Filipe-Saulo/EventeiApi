namespace Api.Models.Dto
{
    public class EventoDto
    {
        public int EventoId { get; set; }
        public string EventoName { get; set; }
        public string Description { get; set; }
        public DateTime DateEvento { get; set; }
        public string Localization { get; set; }
        public string Category { get; set; }
        public int TicketsQuantity { get; set; }
        public List<PhotoDto> Photos { get; set; }
        public List<ComentarioEventoDto> Comentarios { get; set; }
    }

    public class CreateEventoDto
    {
        
        public string EventoName { get; set; }
        public string Description { get; set; }
        public DateTime DateEvento { get; set; }
        public string Localization { get; set; }
        public string Category { get; set; }
        public int TicketsQuantity { get; set; }
        public List<PhotoDto> Photos { get; set; }
        
    }
}
