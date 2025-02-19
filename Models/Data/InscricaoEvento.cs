using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Eventei_Api.Models.Data;

namespace Api.Models.Data
{
    public class InscricaoEvento
    {
        [Key, Column("inscricao_evento_id", TypeName ="INT(11)")]
        public int InscricaoEventoId { get; set; }
        [Column("data_inscricao", TypeName = "TIMESTAMP")]
        public DateTime DataInscricao { get; set; }
        [Column("participou")]
        public bool Participou { get; set; } = false; // false = Inscrito, true = Já participou
        [Column("created_at", TypeName = "TIMESTAMP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at", TypeName = "TIMESTAMP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }

        // Chaves estrangeiras
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Evento")]
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
    }
}
