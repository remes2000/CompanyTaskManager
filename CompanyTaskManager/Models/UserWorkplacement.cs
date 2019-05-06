using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyTaskManager.Models
{
    public class UserWorkplacement
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WorkplacementId { get; set; }
        public bool CanManageTasks { get; set; }
    }
}
