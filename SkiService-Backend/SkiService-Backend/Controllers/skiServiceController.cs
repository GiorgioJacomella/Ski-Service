using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkiService_Backend.Models;


[Route("api/[controller]")]
[ApiController]
public class RegistrationController : ControllerBase
{
    private readonly registrationContext _context;

    public RegistrationController(registrationContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Basic Get API endpoint
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Registration>>> GetRegistrations()
    {
        return await _context.Registrations.ToListAsync();
    }

    /// <summary>
    /// Basic get by ID endpoint
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Registration>> GetRegistration(int id)
    {
        var registration = await _context.Registrations.FindAsync(id);

        if (registration == null)
        {
            return NotFound();
        }

        return registration;
    }

    /// <summary>
    /// Basic Post Endpoint um neue Aufträge zu Regestrieren
    /// </summary>
    /// <param name="registration">Registration model Informationen</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Registration>> PostRegistration(Registration registration)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (registration.Status != "Unfinished")
        {
            return BadRequest("Status must be 'Unfinished'");
        }
        if (registration.Note != "")
        {
            return BadRequest("Note must be an empty string");
        }

        _context.Registrations.Add(registration);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRegistration), new { id = registration.Id }, registration);
    }


    /// <summary>
    /// Put endpoint um daten zu bearbeiten, hier ohne JWT überprüfung
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="registration">Registration model Informationen</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRegistration(int id, Registration registration)
    {
        if (id != registration.Id)
        {
            return BadRequest();
        }

        _context.Entry(registration).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RegistrationExists(id))
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
    /// Basic Delete endpoint, hier ohne JWT übrprüfung
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegistration(int id)
    {
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