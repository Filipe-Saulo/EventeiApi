using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api.Models.Data
{
    public class Photo
    {
        [Key, Column("photo_id", TypeName = "INT(11)")]
        public int PhotoId { get; set; }

        [Required, Column("url_photo", TypeName ="VARCHAR(50000)")]
        public string UrlPhoto { get; set; }
        [Column("pos_evento")]
        public bool PosEvento { get; set; } = false; // false = Foto pré-evento, true = Foto pós-evento
        [Column("created_at", TypeName = "TIMESTAMP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at", TypeName = "TIMESTAMP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }

        // Chave estrangeira
        [ForeignKey("Evento")]
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
    }
}
