using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyTaskManager.Models.RequestBodies
{
    public class RequestChangeStatus
    {
        [Required]
        public String Status { get; set; }
    }
}
