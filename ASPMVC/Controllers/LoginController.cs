using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ASPMVC.DTO;
using System.Security.Cryptography;
using System.Text;
using ASPMVC.MySQL;
using ASPMVC.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;

namespace ASPMVC.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly SqlService _sqlService;
        private readonly JwtSettings _jwtSettings;

        public LoginController(SqlService sqlService, IOptions<JwtSettings> jwtSettings)
        {
            this._sqlService = sqlService;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto model)
        {
            UserModel? user = _sqlService.GetUser(model.Username);
            // If no user with that username is found
            if(user == null)
            {
                return Unauthorized();
            }
            // Check if password matches
            if (IsValidUser(user, model.Password))
            {
                // Username and password matched, generate a token and send it to the client
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                    [
                        new Claim(ClaimTypes.Name, model.Username)
                    ]),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Audience = _jwtSettings.Audience,
                    Issuer = _jwtSettings.Issuer,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new { Token = tokenHandler.WriteToken(token) });
            }

            return Unauthorized();
        }

        bool IsValidUser(UserModel model, string Password) {
            // Convert the salt to a byte array
            var saltBytes = Convert.FromBase64String(model.SaltContrasena);

            // Compute the hash of the input password
            using var hmac = new HMACSHA256(saltBytes);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));

            // Check if the two hashes match
            var computedHashBase64 = Convert.ToBase64String(computedHash);
            return computedHashBase64 == model.HashContrasena;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] CreateUserDto model)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
                return BadRequest("MISSING_FIELDS");

            // Check if user already exists
            if (_sqlService.UserExists(model.Username))
                return BadRequest("USER_ALREADY_EXISTS");

            // Generate salt and hash
            using var hmac = new HMACSHA256();
            var salt = Convert.ToBase64String(hmac.Key);
            var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password)));

            // Add user to the database
            _sqlService.CreateUser(new UserModel(0, model.Username, hash, salt));

            return Ok("USER_REGISTERED_SUCESSFULLY");
        }

    }
}
