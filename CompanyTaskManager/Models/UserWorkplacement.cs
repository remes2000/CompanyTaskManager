using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyTaskManager.Models
{
    public class UserWorkplacement
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public bool CanManageTasks { get; set; }

        public int WorkplacementId { get; set; }
        public Workplacement Workplacement { get; set; }
    }
}
