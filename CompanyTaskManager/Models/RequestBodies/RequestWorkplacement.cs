using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyTaskManager.Models.RequestBodies
{
    public class RequestWorkplacement
    {
        [Required]
        public String Title;
        [Required]
        public String Description;
    }
}
