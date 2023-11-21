using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SkiService_Backend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace SkiService_Backend.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly registrationContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationController(registrationContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Die informationen für das Loginmodel müssein eingegeben werden und werden überprüft, bei korrekter eingabe wird ein JWT zurückgegeben der zusätzlich in UserSessions gespeichert wird
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> PostLogin(LoginModel loginModel)
        {
            var userInfo = await _context.UserInfos.FirstOrDefaultAsync(u => u.UserName == loginModel.UserName);

            if (userInfo != null && userInfo.Password == loginModel.Password)
            {
                var token = GenerateJwtToken(userInfo);
                var userSession = new UserSession
                {
                    SessionKey = token,
                    UserId = userInfo.Id,
                };
                _context.UserSessions.Add(userSession);
    
                _context.SaveChanges();

                return Ok(token);
            }
            else
            {
                return Unauthorized("Invalid username or password.");
            }
        }

        /// <summary>
        /// Generierung des JWT
        /// </summary>
        /// <param name="userInfo">Nutzerinformationen aus der Datenbank</param>
        /// <returns></returns>
        private string GenerateJwtToken(UserInfo userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}