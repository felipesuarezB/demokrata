using System.ComponentModel.DataAnnotations;

namespace Demokrata.Contracts
{
    public class UserDTO
    {
        public int? Id { get; set; } 

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        public string SecondName { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        public string LastName { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        public string SecondLastName { get; set; }

        public DateTime BirthDay { get; set; }

        public double Salary { get; set; }
    }
}
