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
    [Migration("20190506162154_CreateUserWorkplacementTable")]
    partial class CreateUserWorkplacementTable
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Surname");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CompanyTaskManager.Models.UserWorkplacement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CanManageTasks");

                    b.Property<int>("UserId");

                    b.Property<int>("WorkplacementId");

                    b.HasKey("Id");

                    b.ToTable("UsersWorkplacements");
                });

            modelBuilder.Entity("CompanyTaskManager.Models.Workplacement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("OwnerId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Workplacements");
                });
#pragma warning restore 612, 618
        }
    }
}
