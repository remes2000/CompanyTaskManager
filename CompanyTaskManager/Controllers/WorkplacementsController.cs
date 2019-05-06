using CompanyTaskManager.Models;
using CompanyTaskManager.Models.RequestBodies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyTaskManager.Controllers
{
    [Produces("application/json")]
    [Route("api/Workplacements")]
    public class WorkplacementsController : Controller
    {
        private readonly CompanyTaskManagerContext _context;
        private readonly IConfiguration _config;

        public WorkplacementsController(CompanyTaskManagerContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateWorkplacement([FromBody] RequestWorkplacement requestWorkplacement)
        {
            //check if request's body is valid
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

            Workplacement newWorkplacement = new Workplacement
            {
                Title = requestWorkplacement.Title,
                Description = requestWorkplacement.Description,
                OwnerId = userId
            };

            _context.Workplacements.Add(newWorkplacement);
            _context.SaveChanges();

            return Ok( new { message = "Nowe miejsce pracy zostało pomyślnie zarejestrowane! ", id = newWorkplacement.Id});
        }
    }
}
