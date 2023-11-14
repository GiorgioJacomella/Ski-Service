using System.ComponentModel.DataAnnotations;

namespace SkiService_Backend.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
