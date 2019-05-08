using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyTaskManager.Models
{
    public class Task
    {
        public int TaskId { get; set; }

        public int WorkplacementId { get; set; }
        public Workplacement Workplacement { get; set; }

        public int EmployeeId { get; set; }
        public User Employee { get; set; }

        public int AddedById { get; set; }
        public User AddedBy { get; set; }

        public String Title { get; set; }
        public String Description { get; set; }
        public String Priority { get; set; }
        public String Status { get; set; }
        public DateTime AddDate { get; set; }

    }
}
