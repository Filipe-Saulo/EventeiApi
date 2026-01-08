using Api.Util.Validators.DtoValidators;
using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dto.WebUserLogin
{
    public class CreateUserDto : LoginDto
    {                
        [StringLength(30, ErrorMessage = "O nome completo deve ter no maximo 30 caracteres")]
        [Required(ErrorMessage = "O primeiro nome é obrigatório.")]
        public string FullName { get; set; }      
        [PhoneValidatorCustom]
        [Required(ErrorMessage = "O celular é obrigatório.")]
        public string PhoneNumber { get; set; }
        [CpfValidatorCustom]
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string Cpf { get; set; }        
    }
}
