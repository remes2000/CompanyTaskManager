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
    [Route("api/Tasks")]
    public class TasksController : Controller
    {
        private readonly CompanyTaskManagerContext _context;
        private readonly IConfiguration _config;

        public TasksController(CompanyTaskManagerContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateTask([FromBody] RequestTask requestTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (requestTask.Priority != "highest" && requestTask.Priority != "high" && requestTask.Priority != "normal" && requestTask.Priority != "low")
                return BadRequest();

            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

            var workplacement = _context.Workplacements.SingleOrDefault(w => w.WorkplacementId == requestTask.WorkplacementId);
            if (workplacement == null)
                return NotFound();

            var employee = _context.Users.SingleOrDefault(e => e.UserId == requestTask.EmployeeId);
            if (employee == null && requestTask.EmployeeId != -1)
                return NotFound();

            var addedBy = _context.Users.SingleOrDefault(a => a.UserId == requestTask.AddedById);
            if (addedBy == null)
                return NotFound();

            // Now check if can manage tasks
            var canManageTasks = _context.UserWorkplacements.Single(uw => uw.UserId == userId && uw.WorkplacementId == workplacement.WorkplacementId).CanManageTasks;
            if (!canManageTasks)
                return Unauthorized();

            int? employeeId = requestTask.EmployeeId;
            if (employeeId == -1)
                employeeId = null;

            Models.Task newTask = new Models.Task
            {
                WorkplacementId = requestTask.WorkplacementId,
                EmployeeId = employeeId,
                AddedById = requestTask.AddedById,
                Title = requestTask.Title,
                Description = requestTask.Description,
                Priority = requestTask.Priority,
                AddDate = DateTime.Now,
                Status = "todo"
            };
            _context.Tasks.Add(newTask);
            _context.SaveChanges();

            return Ok(new { message = "Nowe zadanie zostało pomyślnie utworzone!" });
        }

        [HttpPut]
        [Authorize]
        [Route("/api/[controller]/assign/{taskId}/{employeeId}")]
        public IActionResult AssignUserToTask(int taskId, int employeeId)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            var task = _context.Tasks
                .Include(t => t.Workplacement)
                    .ThenInclude(w => w.UserWorkplacements)
                .SingleOrDefault(t => t.TaskId == taskId);

            if (task == null)
                return NotFound();

            var relation = task.Workplacement.UserWorkplacements.SingleOrDefault(uw => uw.UserId == userId);
            if (relation == null)
                return Unauthorized();

            if (!relation.CanManageTasks)
                return Unauthorized();

            var doesEmployeeExists = task.Workplacement.UserWorkplacements.Any(uw => uw.UserId == employeeId);
            if (!doesEmployeeExists)
                return NotFound();

            task.EmployeeId = employeeId;
            _context.SaveChanges();

            return Ok(new { message = "Zadanie zostało pomyślnie przypisane"});
        }

        [HttpGet]
        [Authorize]
        [Route("/api/[controller]/{workplacementId}/{userId}")]
        public IActionResult GetTasks(int workplacementId, int userId )
        {
            var tasks = _context.Tasks
                .Include(t => t.Employee)
                .Include(t => t.AddedBy)
                .Where(t => t.EmployeeId == userId && t.WorkplacementId == workplacementId);

            return Ok(tasks);
        }

        [HttpGet]
        [Authorize]
        [Route("/api/[controller]/{workplacementId}/freetasks")]
        public IActionResult GetFreeTasks(int workplacementId)
        {
            var tasks = _context.Tasks
                .Include(t => t.AddedBy)
                .Include(t => t.Employee)
                .Where(t => t.WorkplacementId == workplacementId && t.EmployeeId == null);

            return Ok(tasks);
        }

        [HttpPost]
        [Route("/api/[controller]/changestatus/{taskId}")]
        public IActionResult ChangeStatus(int taskId, [FromBody] RequestChangeStatus requestChangeStatus)
        {
            var task = _context.Tasks
                .SingleOrDefault(t => t.TaskId == taskId);
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

            if (task == null)
                return NotFound();

            if (userId != task.EmployeeId)
                return Unauthorized();

            if (requestChangeStatus.Status != "done" && requestChangeStatus.Status != "doing" && requestChangeStatus.Status != "done")
                return BadRequest();

            task.Status = requestChangeStatus.Status;
            _context.SaveChanges();

            return Ok(new { message = "Status został pomyślnie zmieniony"});
        }

        [HttpDelete]
        [Authorize]
        [Route("/api/[controller]/{taskId}")]
        public IActionResult DeleteTask(int taskId)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            var task = _context.Tasks
                .Include(t => t.Workplacement)
                    .ThenInclude(w => w.UserWorkplacements)
                .SingleOrDefault(t => t.TaskId == taskId);

            if (task == null)
                return NotFound();

            //now check privillages
            var userWorkplacementRelation = task.Workplacement.UserWorkplacements.SingleOrDefault(u => u.UserId == userId);
            if (userWorkplacementRelation == null)
                return NotFound();

            if (!userWorkplacementRelation.CanManageTasks)
                return Unauthorized();

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return Ok(new { message = "Zadanie zostało pomyślnie usunięte" });
        }

        [HttpPut]
        [Authorize]
        [Route("/api/[controller]/markasfree/{taskId}")]
        public IActionResult MarkAsFreeTask(int taskId)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            var task = _context.Tasks
                .Include(t => t.Employee)
                .Include(t => t.Workplacement)
                    .ThenInclude(w => w.UserWorkplacements)
                .SingleOrDefault(t => t.TaskId == taskId);

            var relation = task.Workplacement.UserWorkplacements.SingleOrDefault(uw => uw.UserId == userId);
            if (relation == null)
                return Unauthorized();

            if (!relation.CanManageTasks)
                return Unauthorized();

            task.EmployeeId = null;
            task.Status = "todo";
            _context.SaveChanges();

            return Ok(new { message = "Zadanie zostało oznaczone jako nieprzypisane" });
        }
    }
}
