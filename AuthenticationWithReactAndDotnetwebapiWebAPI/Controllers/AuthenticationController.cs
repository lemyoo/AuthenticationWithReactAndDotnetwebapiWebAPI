using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthenticationWithReactAndDotnetwebapiWebAPI.Data;
using AuthenticationWithReactAndDotnetwebapiWebAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace AuthenticationWithReactAndDotnetwebapiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserAPIDbContext _userAPIDbContext;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserAPIDbContext userAPIDbContext, IConfiguration configuration)
        {
            _userAPIDbContext = userAPIDbContext;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(AddUserRequest request)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                GivenName = request.GivenName,
                Role = request.Role,
                EmailAddress = request.EmailAddress,
                Surname = request.Surname,
                Password = request.Password,
            };
            await _userAPIDbContext.Users.AddAsync(user);
            await _userAPIDbContext.SaveChangesAsync();
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public IActionResult SignIn([FromBody] LoginUserRequest request)
        {
            var user = Authenticate(request);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var token = GenerateToken(user);

            return Ok(token);
        }

        /* TODO: I need to do forgot password*/
        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName!),
                new Claim(ClaimTypes.Email, user.EmailAddress!),
                new Claim(ClaimTypes.GivenName, user.GivenName!),
                new Claim(ClaimTypes.Surname, user.Surname!),
                new Claim(ClaimTypes.Role, user.Role!),
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User? Authenticate(LoginUserRequest request)
        {
            var user = _userAPIDbContext.Users.FirstOrDefault(x => x.UserName.ToLower() == request.UserName.ToLower() && x.Password == request.Password);
            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
}
