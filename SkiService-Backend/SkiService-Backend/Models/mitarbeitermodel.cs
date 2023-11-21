using System.ComponentModel.DataAnnotations;

namespace SkiService_Backend.Models
{
    /// <summary>
    /// Model für JWT prüfung
    /// </summary>
    public class mitarbeitermodel
    {
        [Required]
        public string Token { get; set; }
    }
}
