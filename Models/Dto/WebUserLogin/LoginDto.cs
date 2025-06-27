using Api.Util.Validators.DtoValidators;
using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dto.WebUserLogin
{
    public class LoginDto
    {               
        [Required, EmailAddress]
        [EmailValidatorCustom]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }                    
    }
}
