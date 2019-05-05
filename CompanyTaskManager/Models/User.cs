using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CompanyTaskManager.Models
{
    public class User
    {
        public int Id { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String PasswordHash { get; set; }
    }
}
