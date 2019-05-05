using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyTaskManager.Models
{
    public class CompanyTaskManagerContext : DbContext
    {
        public CompanyTaskManagerContext(DbContextOptions<CompanyTaskManagerContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
