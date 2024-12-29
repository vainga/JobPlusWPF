using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using JobPlusWPF.Model.Classes;

namespace JobPlusWPF.DBLogic
{
    public class AppDbContext : DbContext
    {
        //Кносоль диспетчера пакетов
        //Add-Migration UpdateUserRole
        //Update-Database

        //Дописать по мере появления новых классов =
        //public DbSet<MyEntity> MyEntities { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<JobSeeker> JobSeekers { get; set; }
        public DbSet<CityDirectory> Cities { get; set; }
        public DbSet<StreetDirectory> Streets { get; set; }
        public DbSet<EducationLevel> EducationLevels { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Benefit> Benefits { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=JobPlusDB;Username=postgres;Password=20110409");
        }

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public void SeedAdmin()
        {
            if (!Users.Any())
            {
                string adminPassword = "admin";
                var adminUser = new User("admin", adminPassword, Role.Admin);
                Users.Add(adminUser);
                SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobSeeker>()
                .HasOne(j => j.City)
                .WithMany(c => c.JobSeekers)
                .HasForeignKey(j => j.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobSeeker>()
                .HasOne(j => j.Street)
                .WithMany(s => s.JobSeekers)
                .HasForeignKey(j => j.StreetId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobSeeker>()
                .HasOne(j => j.EducationLevel)
                .WithMany(e => e.JobSeekers)
                .HasForeignKey(j => j.EducationLevelId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<JobSeeker>()
            //    .Property(j => j.StatusId)
            //    .HasDefaultValue(1); 

            modelBuilder.Entity<JobSeeker>()
                .HasOne(j => j.Status)
                .WithMany(s => s.JobSeekers)
                .HasForeignKey(j => j.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobSeeker>()
                .HasOne(j => j.User)
                .WithMany(u => u.JobSeekers)
                .HasForeignKey(j => j.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employer>()
                .HasOne(e => e.City)
                .WithMany()
                .HasForeignKey(e => e.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employer>()
                .HasOne(e => e.Street)
                .WithMany()
                .HasForeignKey(e => e.StreetId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employer>()
                .HasOne(e => e.User)
                .WithMany(u => u.Employers)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobSeeker>()
                .HasOne(j => j.User)
                .WithMany(u => u.JobSeekers)
                .HasForeignKey(j => j.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vacancy>()
                .HasOne(v => v.Employer)
                .WithMany()
                .HasForeignKey(v => v.EmployerId)  
                .OnDelete(DeleteBehavior.Restrict);
        }



        public void SeedEducationLevels()
        {
            if (!EducationLevels.Any())
            {
                var educationLevels = new List<EducationLevel>
        {
            new EducationLevel("Дошкольное образование"),
            new EducationLevel("Начальное общее образование"),
            new EducationLevel("Основное общее образование"),
            new EducationLevel("Среднее общее образование"),
            new EducationLevel("Среднее профессиональное образование"),
            new EducationLevel("Бакалавриат"),
            new EducationLevel("Специалитет, магистратура"),
            new EducationLevel("Подготовка кадров высшей квалификации"),
            new EducationLevel("Дополнительное образование детей и взрослых"),
            new EducationLevel("Дополнительное профессиональное образование")
        };

                EducationLevels.AddRange(educationLevels);

                SaveChanges();
            }
        }

        public void SeedStatuses()
        {
            if (!Statuses.Any())
            {
                var statuses = new List<Status>
            {
                new Status("В работе"),
                new Status("В архиве"),
                new Status("На пособии")
            };

                Statuses.AddRange(statuses);

                SaveChanges();
            }
        }

    }


}
