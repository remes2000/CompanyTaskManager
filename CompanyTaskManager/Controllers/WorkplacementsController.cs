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

            //Add user to workplacement
            UserWorkplacement userWorkplacement = new UserWorkplacement
            {
                UserId = userId,
                WorkplacementId = newWorkplacement.Id,
                CanManageTasks = true
            };
            _context.UsersWorkplacements.Add(userWorkplacement);
            _context.SaveChanges();

            return Ok( new { message = "Nowe miejsce pracy zostało pomyślnie zarejestrowane! ", id = newWorkplacement.Id});
        }

        [HttpGet]
        [Authorize]
        [Route("/api/[controller]/{id}")]
        public IActionResult GetWorkplacement(int id)
        {
            var workplacement = _context
                .Workplacements
                .SingleOrDefault(w => w.Id == id);

            if (workplacement == null)
                return NotFound();

            return Ok(workplacement);
        }

        [HttpGet]
        [Authorize]
        [Route("/api/[controller]/myworkplacements")]
        public IActionResult GetMyWorkplacements()
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            var workplacementsIds = _context.UsersWorkplacements
                .Where(uw => uw.UserId == userId)
                .Select(uw => uw.WorkplacementId)
                .ToList();

            if(!workplacementsIds.Any())
            {
                return NotFound();
            }

            List<Workplacement> workplacements = new List<Workplacement>();
            foreach (int id in workplacementsIds)
            {
                workplacements.Add(_context.Workplacements.Single(w => w.Id == id));
            }
            
            return Ok(workplacements);
        }

        [HttpGet]
        [Authorize]
        [Route("/api/[controller]/members/{workplacementId}")]
        public IActionResult GetWorkplacementMembers(int workplacementId)
        {
            var usersIds = _context.UsersWorkplacements
                .Where(uw => uw.WorkplacementId == workplacementId)
                .Select(uw => uw.UserId)
                .ToList();

            if(!usersIds.Any())
            {
                return NotFound();
            }

            List<User> users = new List<User>();
            User user;
            foreach (int id in usersIds)
            {
                user = _context.Users.Single(u => u.Id == id);
                user.PasswordHash = null;
                user.Email = null;

                users.Add(user);
            }

            return Ok(users);
        }
    }
}
