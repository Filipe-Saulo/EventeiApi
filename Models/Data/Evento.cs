using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Eventei_Api.Models.Data;

namespace Api.Models.Data
{
    public class Evento
    {
        [Key, Column("evento_id", TypeName = "INT(11)")]
        public int EventoId { get; set; }

        [Required, StringLength(255), Column("evento_name", TypeName = "VARCHAR(255)")]        
        public string EventoName { get; set; }

        [Required, Column("description", TypeName = "VARCHAR(255)")]
        public string Description { get; set; }

        [Required, Column("date_evento", TypeName = "TIMESTAMP")]
        public DateTime DateEvento { get; set; }

        [Required, Column("localization", TypeName = "VARCHAR(255)"), StringLength(255)]       
        public string localization { get; set; }

        [Required, Column("category", TypeName = "VARCHAR(100)"), StringLength(100)]
        public string Category { get; set; }

        [Required, Column("tickets_quantity", TypeName = "INT(11)")]
        public int TicketsQuantity { get; set; }
        [Column("created_at", TypeName = "TIMESTAMP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at", TypeName = "TIMESTAMP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }

        // Chave estrangeira (Usuário que criou o evento)
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        // Relacionamentos
        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
        public ICollection<InscricaoEvento> Inscricoes { get; set; } = new List<InscricaoEvento>();
        public ICollection<ComentarioEvento> Comentarios { get; set; } = new List<ComentarioEvento>();
    }
}
