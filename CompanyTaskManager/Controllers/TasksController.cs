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
            if (employee == null)
                return NotFound();

            var addedBy = _context.Users.SingleOrDefault(a => a.UserId == requestTask.AddedById);
            if (addedBy == null)
                return NotFound();

            // Now check if can manage tasks
            var canManageTasks = _context.UserWorkplacements.Single(uw => uw.UserId == userId && uw.WorkplacementId == workplacement.WorkplacementId).CanManageTasks;
            if (!canManageTasks)
                return Unauthorized();

            Models.Task newTask = new Models.Task
            {
                WorkplacementId = requestTask.WorkplacementId,
                EmployeeId = requestTask.EmployeeId,
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

        [HttpGet]
        [Route("/api/[controller]/{workplacementId}/{userId}")]
        public IActionResult GetTasks(int workplacementId, int userId )
        {
            var tasks = _context.Tasks
                .Include(t => t.Employee)
                .Include(t => t.AddedBy)
                .Where(t => t.EmployeeId == userId && t.WorkplacementId == workplacementId);

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
    }
}
