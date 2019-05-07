using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyTaskManager.Models.RequestBodies
{
    public class RequestAddMember
    {
        [Required]
        public String Username { get; set; }
        [Required]
        public int WorkplacementId { get; set; }
        [Required]
        public bool CanManageTasks { get; set; }
    }
}
