using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyTaskManager.Models
{
    public class Workplacement
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public int OwnerId { get; set; }
    }
}
