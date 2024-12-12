using System.ComponentModel.DataAnnotations;

namespace Finale_Crud.Models
{
    public class LoginViewModel
    {
            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        

    }
}
