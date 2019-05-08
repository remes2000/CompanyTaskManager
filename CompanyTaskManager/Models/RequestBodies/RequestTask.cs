using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyTaskManager.Models.RequestBodies
{
    public class RequestTask
    {
        [Required]
        public String Title { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]
        public String Priority { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int AddedById { get; set; }
        [Required]
        public int WorkplacementId { get; set; }
    }
}
