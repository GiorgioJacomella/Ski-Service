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

        [HttpPut("registration/{id}")]
        public async Task<IActionResult> PutRegistration(int id, [FromBody] Registration registration, string token)
        {
            // Token authentication
            var userSession = await _context.UserSessions.FirstOrDefaultAsync(us => us.SessionKey == token);
            if (userSession == null)
            {
                return BadRequest("Invalid session token");
            }

            if (id != registration.Id)
            {
                return BadRequest();
            }

            _context.Entry(registration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(registration);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Registrations.Any(r => r.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete("registration/{id}")]
        public async Task<IActionResult> DeleteRegistration(int id, string token)
        {
            // Token authentication
            var userSession = await _context.UserSessions.FirstOrDefaultAsync(us => us.SessionKey == token);
            if (userSession == null)
            {
                return BadRequest("Invalid session token");
            }

            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }

            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registrations.Any(e => e.Id == id);
        }


    }
}