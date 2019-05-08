using CompanyTaskManager.Models;
using CompanyTaskManager.Models.RequestBodies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                WorkplacementId = newWorkplacement.WorkplacementId,
                CanManageTasks = true
            };
            _context.UserWorkplacements.Add(userWorkplacement);
            _context.SaveChanges();

            return Ok( new { message = "Nowe miejsce pracy zostało pomyślnie zarejestrowane! ", id = newWorkplacement.WorkplacementId});
        }

        [HttpGet]
        [Authorize]
        [Route("/api/[controller]/{id}")]
        public IActionResult GetWorkplacement(int id)
        {
            var workplacement = _context
                .Workplacements
                .SingleOrDefault(w => w.WorkplacementId == id);

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
            var workplacements =
                _context.Workplacements
                .Where(w =>
                    w.UserWorkplacements.Any(uw => uw.UserId == userId)
                );

            return Ok(workplacements);
        }

        [HttpGet]
        [Authorize]
        [Route("/api/[controller]/members/{workplacementId}")]
        public IActionResult GetWorkplacementMembers(int workplacementId)
        {
            var members = _context
                .Users
                .Where(u => u.UserWorkplacements.Any(uw => uw.WorkplacementId == workplacementId));

            return Ok(members);
        }

        [HttpPost]
        [Authorize]
        [Route("/api/[controller]/addmember")]
        public IActionResult AddWorkplacementsMember([FromBody] RequestAddMember requestAddMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            var workplacement = _context
                .Workplacements
                .SingleOrDefault(w => w.WorkplacementId == requestAddMember.WorkplacementId);

            if (workplacement == null)
                return NotFound();

            if (workplacement.OwnerId != userId)
                return Unauthorized();

            var member = _context
                .Users
                .SingleOrDefault(u => u.Username == requestAddMember.Username);

            if (member == null)
                return Ok(new { error = "Taki użytkownik nie istnieje!" });

            var relation = _context.UserWorkplacements
                .SingleOrDefault(uw => uw.UserId == member.UserId && uw.WorkplacementId == workplacement.WorkplacementId);

            if (relation != null)
                return Ok(new { error = "Ten użytkownik jest już twoim pracownikiem!" });

            _context.UserWorkplacements.Add(new UserWorkplacement
            {
                UserId = member.UserId,
                WorkplacementId = workplacement.WorkplacementId,
                CanManageTasks = requestAddMember.CanManageTasks
            });
            _context.SaveChanges();

            return Ok(new { message = "Użytkownik został pomyślnie dodany do miejsca pracy!" });
        }

        [HttpGet]
        [Authorize]
        [Route("/api/[controller]/canmanagetasks/{userId}/{workplacementId}")]
        public IActionResult CheckIfCanManageTasks(int userId, int workplacementId)
        {
            var relation = _context
                .UserWorkplacements
                .SingleOrDefault(uw => uw.UserId == userId && uw.WorkplacementId == workplacementId);

            if (relation == null)
                return NotFound();

            return Ok(relation.CanManageTasks);
        }
    }
}
