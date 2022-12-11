using System.ComponentModel.DataAnnotations;

namespace CRUDwithEF.DTOs.Auth
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
