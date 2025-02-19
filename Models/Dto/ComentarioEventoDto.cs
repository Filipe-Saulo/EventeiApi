namespace Api.Models.Dto
{
    public class ComentarioEventoDto
    {
        public int ComentarioEventoId { get; set; }
        public string Comentario { get; set; }
        public DateTime DataComentario { get; set; }
        public string UsuarioNome { get; set; }
    }
}
