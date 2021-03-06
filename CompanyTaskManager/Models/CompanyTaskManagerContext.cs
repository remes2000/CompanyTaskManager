﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyTaskManager.Models
{
    public class CompanyTaskManagerContext : DbContext
    {
        public CompanyTaskManagerContext(DbContextOptions<CompanyTaskManagerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserWorkplacement>().HasKey(uw => new { uw.UserId, uw.WorkplacementId });

            modelBuilder.Entity<User>()
                .HasMany<Workplacement>(u => u.OwnedWorkplacements)
                .WithOne(w => w.Owner)
                .HasForeignKey(w => w.OwnerId);

            modelBuilder.Entity<Task>()
                .HasOne<Workplacement>(t => t.Workplacement)
                .WithMany(w => w.Tasks)
                .HasForeignKey(t => t.WorkplacementId);

            modelBuilder.Entity<Task>()
                .HasOne<User>(t => t.Employee)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.EmployeeId);

            modelBuilder.Entity<Task>()
                .HasOne<User>(t => t.AddedBy)
                .WithMany(u => u.AddedTasks)
                .HasForeignKey(t => t.AddedById);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Workplacement> Workplacements { get; set; }
        public DbSet<UserWorkplacement> UserWorkplacements { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
