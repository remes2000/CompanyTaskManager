﻿// <auto-generated />
using CompanyTaskManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CompanyTaskManager.Migrations
{
    [DbContext(typeof(CompanyTaskManagerContext))]
    [Migration("20190508093041_CreateCanManageTasksField")]
    partial class CreateCanManageTasksField
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("CompanyTaskManager.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Surname");

                    b.Property<string>("Username");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CompanyTaskManager.Models.UserWorkplacement", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("WorkplacementId");

                    b.Property<bool>("CanManageTasks");

                    b.HasKey("UserId", "WorkplacementId");

                    b.HasIndex("WorkplacementId");

                    b.ToTable("UserWorkplacements");
                });

            modelBuilder.Entity("CompanyTaskManager.Models.Workplacement", b =>
                {
                    b.Property<int>("WorkplacementId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("OwnerId");

                    b.Property<string>("Title");

                    b.HasKey("WorkplacementId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Workplacements");
                });

            modelBuilder.Entity("CompanyTaskManager.Models.UserWorkplacement", b =>
                {
                    b.HasOne("CompanyTaskManager.Models.User", "User")
                        .WithMany("UserWorkplacements")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CompanyTaskManager.Models.Workplacement", "Workplacement")
                        .WithMany("UserWorkplacements")
                        .HasForeignKey("WorkplacementId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CompanyTaskManager.Models.Workplacement", b =>
                {
                    b.HasOne("CompanyTaskManager.Models.User", "Owner")
                        .WithMany("OwnedWorkplacements")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
