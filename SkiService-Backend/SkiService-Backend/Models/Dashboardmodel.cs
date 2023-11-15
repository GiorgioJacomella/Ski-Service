using System.ComponentModel.DataAnnotations;

namespace SkiService_Backend.Models
{
    public class DashboardModel
    {
        [Required]
        public string Token { get; set; }
    }
}
