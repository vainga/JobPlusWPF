//using JobPlusWPF.Model.Classes;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


//namespace JobPlusWPF.DBLogic
//{
//    public class DatabaseInitializerService
//    {
//        public DbSet<User> Users { get; set; }
//        public DbSet<JobSeeker> JobSeekers { get; set; }
//        public DbSet<CityDirectory> Cities { get; set; }
//        public DbSet<StreetDirectory> Streets { get; set; }
//        public DbSet<EducationLevel> EducationLevels { get; set; }
//        public DbSet<Employer> Employers { get; set; }
//        public DbSet<Vacancy> Vacancies { get; set; }
//        public DbSet<Benefit> Benefits { get; set; }
//        public DbSet<Status> Statuses { get; set; }
//        public DbSet<ArchiveEntry> Archive { get; set; }

//        public void SeedAdmin()
//        {
//            if (!Users.Any())
//            {
//                string adminPassword = "admin";
//                string adminHash = "admin";
//                var adminUser = new User("admin", adminPassword, Role.Admin);
//                Users.Add(adminUser);
//                SaveChanges();
//            }
//        }

//        public void SeedEducationLevels()
//        {
//            if (!EducationLevels.Any())
//            {
//                var educationLevels = new List<EducationLevel>
//        {
//            new EducationLevel("Дошкольное образование"),
//            new EducationLevel("Начальное общее образование"),
//            new EducationLevel("Основное общее образование"),
//            new EducationLevel("Среднее общее образование"),
//            new EducationLevel("Среднее профессиональное образование"),
//            new EducationLevel("Бакалавриат"),
//            new EducationLevel("Специалитет, магистратура"),
//            new EducationLevel("Подготовка кадров высшей квалификации"),
//            new EducationLevel("Дополнительное образование детей и взрослых"),
//            new EducationLevel("Дополнительное профессиональное образование")
//                };

//                EducationLevels.AddRange(educationLevels);

//                SaveChanges();
//            }
//        }

//        public void SeedStatuses()
//        {
//            if (!Statuses.Any())
//            {
//                var statuses = new List<Status>
//            {
//                new Status("В работе"),
//                new Status("В архиве"),
//                new Status("На пособии")
//                };

//                Statuses.AddRange(statuses);

//                SaveChanges();
//            }
//        }
//    }
//}
