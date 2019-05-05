using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyTaskManager.Models.RequestBodies
{
    public class UserCredentials
    {
        [Required]
        public String Username {get; set;}
        [Required]
        public String Password { get; set; }
    }
}
