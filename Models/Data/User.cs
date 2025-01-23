using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventei_Api.Models.Data
{
    public class User : IdentityUser
    {
        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("middle_name")]
        public string MiddleName { get; set; }

        [Column("cpf")]
        public string Cpf { get; set; }

        [Column("intro")]
        public string Intro { get; set; }

        [Column("profile")]
        public string Profile { get; set; }

        [Column("register_code")]
        public string RegisterCode { get; set; }

        [Column("app_qr_key")]
        public string AppQrKey { get; set; }

        [Column("registered_at")]
        public DateTime RegisteredAt { get; set; } 

        [Column("last_login")]
        public DateTime LastLogin { get; set; } 
    }
}
