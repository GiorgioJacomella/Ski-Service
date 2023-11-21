using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiService_Backend.Models;

namespace SkiService_Backend.Controllers
{
    [Route("api/mitarbeiter")]
    [ApiController]
    public class mitarbeiterController : ControllerBase
    {
        private readonly registrationContext _context;
        private readonly IConfiguration _configuration;


        public mitarbeiterController(registrationContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Verweis auf mitarbeiterdmodel wobei ein JTW Mitgegeben werden soll
        /// </summary>
        /// <param name="mitarbeiterModel">JWToken</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostDashboard([FromBody] mitarbeitermodel dashboardModel)
        {
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

        /// <summary>
        /// Eine bestimmte ID einer registration wird abgesucht und verändert, zusätzlich muss ein Gültiger JWT Vorliegen
        /// </summary>
        /// <param name="id">Object ID</param>
        /// <param name="registration">Registration</param>
        /// <param name="token">JWToken</param>
        /// <returns></returns>
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

        /// <summary>
        /// Eine bestimmte ID wird gelöscht, zusätzlich muss ein Gültiger JWT vorliegen
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="token">JWToken</param>
        /// <returns></returns>
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