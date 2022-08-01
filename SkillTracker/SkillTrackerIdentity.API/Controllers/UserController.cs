using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SkillTrackerIdentity.API.Models;
using SkillTrackerIdentity.API.Services;


namespace SkillTrackerIdentity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService Service)
        {
            _userService = Service;
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] User user)
        {           
            var users = _userService.GetUser(user.UserName, user.Password);

            if (users == null)
                return Ok(new { Token = "", Usertype = "0" });
            else
            {
                var claims = new[]{ new Claim(ClaimTypes.Name, users.UserName)};

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretTokenForJwt"));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new { token = tokenHandler.WriteToken(token), User = users.UserName, Usertype = users.Role});
            }
        }
      
    }
    
}
