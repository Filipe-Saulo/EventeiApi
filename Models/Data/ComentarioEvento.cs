using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Eventei_Api.Models.Data;

namespace Api.Models.Data
{
    public class ComentarioEvento
    {
        [Key, Column("comentario_evento_id", TypeName ="INT(11)")]
        public int ComentarioEventoId { get; set; }

        [Required, Column("comentario", TypeName = "VARCHAR(500)")]
        public string Comentario { get; set; }
        [Column("data_comentario", TypeName = "TIMESTAMP")]
        public DateTime DataComentario { get; set; }

        // Chaves estrangeiras
        [ForeignKey("User")]
        public string UsuarioId { get; set; }
        public User User { get; set; }

        [ForeignKey("Evento")]
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
    }
}
