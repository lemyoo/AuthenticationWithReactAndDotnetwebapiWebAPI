﻿using AuthenticationWithReactAndDotnetwebapiWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthenticationWithReactAndDotnetwebapiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("Admins")]
        [Authorize(Roles = "Administrator")]
        public IActionResult AdminsEndpoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi {currentUser!.UserName}, you are an {currentUser.Role}");
        }

        [HttpGet("Users")]
        [Authorize(Roles = "User")]
        public IActionResult UsersEndpoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi {currentUser!.UserName}, you are a {currentUser.Role}");
        }

        [HttpGet("UsersAndAdmins")]
        [Authorize(Roles = "User,Administrators")]
        public IActionResult UsersAndAdministratorsEndpoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi {currentUser!.UserName}, you are a {currentUser.Role}");
        }


        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok("Hi, you are on public property");
        }

        private UserModel? GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if(identity != null)
            {
                var userClaims  = identity.Claims;

                return new UserModel
                {
                    UserName = userClaims.FirstOrDefault(o=>o.Type == ClaimTypes.NameIdentifier)?.Value,
                    EmailAddress = userClaims.FirstOrDefault(o=>o.Type == ClaimTypes.Email)?.Value,
                    GivenName = userClaims.FirstOrDefault(o=>o.Type == ClaimTypes.GivenName)?.Value,
                    Surname = userClaims.FirstOrDefault(o=>o.Type == ClaimTypes.Surname)?.Value,
                    Role = userClaims.FirstOrDefault(o=>o.Type == ClaimTypes.Role)?.Value,
                };
            }
            return null;
        }
    }
}
