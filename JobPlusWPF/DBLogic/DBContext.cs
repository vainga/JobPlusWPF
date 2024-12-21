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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=JobPlusDB;Username=postgres;Password=20110409");
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
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd(); // Устанавливает автоинкремент для Id
        }

    }
}