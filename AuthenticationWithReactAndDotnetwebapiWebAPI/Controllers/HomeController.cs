using AuthenticationWithReactAndDotnetwebapiWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthenticationWithReactAndDotnetwebapiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("Users")]
        [Authorize(Roles = "user")]
        public IActionResult UserEndpoint()
        {
            var user = GetCurrentUser();
            return Ok(user);
        }

        private User? GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            
            if(identity == null)
            {
                return null;
            }
            var userClaims = identity.Claims;
            return new User
            {
                UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier).Value,
                EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email).Value,
                GivenName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName).Value,
                Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname).Value,
                Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role).Value,
            };


        }
    }
}
