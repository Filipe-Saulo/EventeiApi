using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dto
{
    public class ApiUserDto : LoginUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string Cpf { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public ICollection<string> Roles { get; set; }
       
    }
}
