using JobPlusWPF.DBLogic;
using JobPlusWPF.Model.Classes;
using JobPlusWPF.Model.Interfaces;
using JobPlusWPF.Service;
using JobPlusWPF.View;
using JobPlusWPF.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Navigation;

namespace JobPlusWPF
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } // Сделайте свойство публичным

        private ServiceProvider _serviceProvider;

        public App()
        {
            ConfigureServices();
        }

        private void ConfigureServices()
        {
            var services = new ServiceCollection();

            // Регистрация DbContext с Npgsql
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql("Host=localhost;Database=JobPlusDB;Username=postgres;Password=20110409"));

            // Регистрация репозиториев и сервисов
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<JobSeeker>, Repository<JobSeeker>>();
            services.AddScoped<IRepository<Employer>, Repository<Employer>>();
            services.AddScoped<IRepository<Vacancy>, Repository<Vacancy>>();
            services.AddScoped<IRepository<CityDirectory>, Repository<CityDirectory>>();
            services.AddScoped<IRepository<StreetDirectory>, Repository<StreetDirectory>>();
            services.AddScoped<IRepository<EducationLevel>, Repository<EducationLevel>>();

            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IJobSeekerService, JobSeekerService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IEmplyerService, EmplyerService>();
            services.AddScoped<IVacancyService, VacancyService>();


            // Регистрация навигатора
            services.AddSingleton<INavigator, Navigator>();

            // Регистрация ViewModel
            services.AddScoped<EnterViewModel>();
            services.AddScoped<MainViewModel>();
            services.AddScoped<JobSeekerAddViewModel>();
            services.AddTransient<JobSeekerDataGridViewModel>();
            services.AddScoped<EmplyerAddViewModel>();
            services.AddTransient<EmplyerDataGridViewModel>();
            services.AddScoped<AddVacancyViewModel>();
            services.AddTransient<VacancyDataGridViewModel>();

            // Регистрация окон
            services.AddTransient<EnterWindow>();
            services.AddTransient<MainWindow>();
            services.AddTransient<JobSeekerAddWindow>();
            services.AddTransient<AddEmployer>();
            services.AddTransient<AddVacancy>();

            // Строим контейнер зависимостей
            _serviceProvider = services.BuildServiceProvider();

            // Инициализация свойства Services
            Services = _serviceProvider; // Присваиваем свойству значение _serviceProvider
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Выполнение миграций и начальных данных в фоновом потоке
            await Task.Run(() =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    // Выполнение миграций (если нужно)
                    dbContext.Database.Migrate();

                    // Заполнение начальных данных
                    dbContext.SeedAdmin();
                    dbContext.SeedEducationLevels();
                }
            });

            // Создаем окно и передаем ViewModel через DI
            var enterWindow = _serviceProvider.GetRequiredService<EnterWindow>();
            enterWindow.Show();
        }



        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            // Освобождение ресурсов
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }

}
