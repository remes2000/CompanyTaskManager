using CompanyTaskManager.Models;
using CompanyTaskManager.Models.RequestBodies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyTaskManager.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly CompanyTaskManagerContext _context;

        public UsersController(CompanyTaskManagerContext context)
        {
            _context = context;
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
                return Ok(new { error = "This username is already taken!" });

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

            return Ok(new { message = "Succesful registered" });
        }
    }
}
