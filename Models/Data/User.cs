using Api.Models.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventei_Api.Models.Data
{
    public class User : IdentityUser
    {
        [Column("full_name")]
        public string FullName { get; set; }

        [Column("cpf")]
        public string Cpf { get; set; }

        [Column("registered_at")]
        public DateTime RegisteredAt { get; set; } 

        [Column("last_login")]
        public DateTime LastLogin { get; set; }

        public ICollection<InscricaoEvento> Inscricoes { get; set; } = new List<InscricaoEvento>();
        public ICollection<ComentarioEvento> Comentarios { get; set; } = new List<ComentarioEvento>();
    }
}
