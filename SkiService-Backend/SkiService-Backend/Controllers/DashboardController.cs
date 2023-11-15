using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiService_Backend.Models;

namespace SkiService_Backend.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly registrationContext _context;
        private readonly IConfiguration _configuration;

        // Inject the configuration into your controller
        public DashboardController(registrationContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> PostDashboard([FromBody] DashboardModel dashboardModel)
        {
            // Check if the session key exists in the database
            var userSession =  await _context.UserSessions
                                            .FirstOrDefaultAsync(us => us.SessionKey == dashboardModel.Token);

            if (userSession != null)
            {
                var registrations = await _context.Registrations.ToListAsync();
                return Ok(registrations);
            }
            else
            {
                return BadRequest("Session invalid");
            }
        }


    }
}