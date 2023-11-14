using Microsoft.AspNetCore.Mvc;
using skiService.Models;
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

    // GET: api/Registration
    [HttpGet]
    public async Task<ActionResult<IEnumerable<registrations>>> GetRegistrations()
    {
        return await _context.registrations.ToListAsync();
    }

    // GET: api/Registration/5
    [HttpGet("{id}")]
    public async Task<ActionResult<registrations>> GetRegistration(int id)
    {
        var registration = await _context.registrations.FindAsync(id);

        if (registration == null)
        {
            return NotFound();
        }

        return registration;
    }

    // POST: api/Registration
    [HttpPost]
    public async Task<ActionResult<registrations>> PostRegistration(registrations registration)
    {
        _context.registrations.Add(registration);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRegistration), new { id = registration.Id }, registration);
    }

    // PUT: api/Registration/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRegistration(int id, registrations registration)
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

    // DELETE: api/Registration/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegistration(int id)
    {
        var registration = await _context.registrations.FindAsync(id);
        if (registration == null)
        {
            return NotFound();
        }

        _context.registrations.Remove(registration);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RegistrationExists(int id)
    {
        return _context.registrations.Any(e => e.Id == id);
    }

}