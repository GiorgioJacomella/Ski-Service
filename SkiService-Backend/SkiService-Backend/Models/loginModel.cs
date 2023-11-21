using System.ComponentModel.DataAnnotations;

namespace SkiService_Backend.Models
{
    /// <summary>
    /// Model für Mitarbeiter Account login
    /// Wurde mit dem Database first konzept generiert
    /// </summary>
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
