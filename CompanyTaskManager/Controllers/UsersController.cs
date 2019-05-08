using CompanyTaskManager.Models;
using CompanyTaskManager.Models.RequestBodies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CompanyTaskManager.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly CompanyTaskManagerContext _context;
        private readonly IConfiguration _config;

        public UsersController(CompanyTaskManagerContext context, IConfiguration config)
        {
            _context = context;
            _config = config; 
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] RequestUser requestUser)
        {
            //Check if model is valid
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            //Check if username is avaliable 
            var userWithProvidedUsername = _context
                .Users
                .SingleOrDefault(u => u.Username == requestUser.Username);

            if (userWithProvidedUsername != null)
                return Ok(new { error = "Ta nazwa użytkownika jest już zajęta!" });

            User newUser = new User()
            {
                Username = requestUser.Username,
                Name = requestUser.Name,
                Surname = requestUser.Surname,
                Email = requestUser.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(requestUser.Password)
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok(new { message = "Pomyślnie zarejestrowano!" });
        }

        [HttpPost]
        [Route("/api/[controller]/authenticate")]
        public IActionResult AuthenticateUser([FromBody] UserCredentials userCredentials)
        {
            //check if request's body is valid
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //check if credentials are correct
            var user = _context
                .Users
                .SingleOrDefault(u => u.Username == userCredentials.Username && BCrypt.Net.BCrypt.Verify(userCredentials.Password, u.PasswordHash));

            if (user == null)
                return Ok(new { error = "Podano nieprawidłową nazwę użytkownika lub hasło!"});

            string token = BuildToken(user);
            return Ok(new
            {
                UserId = user.UserId,
                Username = user.Username,
                Token = token,
                Name = user.Name,
                Surname = user.Surname
            });
        }

        private string BuildToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Name),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.Surname)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
