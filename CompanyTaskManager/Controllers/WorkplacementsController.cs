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
        [Route("/api/[controller]/ownedby/{ownerId}")]
        public IActionResult GetWorkplacementsOwnedBy(int ownerId)
        {
            var workplacements = _context.Workplacements
                .Where(w => w.OwnerId == ownerId);

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

        [HttpDelete]
        [Authorize]
        [Route("/api/[controller]/deleteemployee/{workplacementId}/{employeeId}")]
        public IActionResult DeleteEmployee(int workplacementId, int employeeId)
        {
            var workplacement = _context.Workplacements
                .Include(w => w.UserWorkplacements)
                .Include(w => w.Tasks)
                .SingleOrDefault(w => w.WorkplacementId == workplacementId);

            if (workplacement == null)
                return NotFound();

            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            if (userId != workplacement.OwnerId)
                return Unauthorized();

            if (employeeId == workplacement.OwnerId)
                return BadRequest();

            if (!workplacement.UserWorkplacements.Any(uw => uw.UserId == employeeId))
                return NotFound();

            //first clear all this user tasks
            //delete done tasks
            var tasks = workplacement.Tasks.Where(t => t.EmployeeId == employeeId);
            foreach( var task in tasks)
            {
                task.EmployeeId = null;
                if (task.Status == "done")
                    _context.Tasks.Remove(task);
                else
                    task.Status = "todo";
            }

            var relation = workplacement.UserWorkplacements.Single(uw => uw.UserId == employeeId);
            _context.UserWorkplacements.Remove(relation);
            _context.SaveChanges();

            return Ok(new { message = "Pracownik został usunięty"});
        }

        [HttpDelete]
        [Authorize]
        [Route("/api/[controller]/{workplacementId}")]
        public IActionResult DeleteWorkplace(int workplacementId)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

            var workplacement = _context.Workplacements.SingleOrDefault(w => w.WorkplacementId == workplacementId);
            if (workplacement == null)
                return NotFound();

            if (workplacement.OwnerId != userId)
                return Unauthorized();

            _context.Workplacements.Remove(workplacement);
            _context.SaveChanges();

            return Ok(new { message = "Miejsce pracy zostało pomyślnie usunięte" });
        }
    }
}
