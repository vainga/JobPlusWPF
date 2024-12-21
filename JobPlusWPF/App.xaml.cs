using JobPlusWPF.DBLogic;
using JobPlusWPF.Model.Classes;
using JobPlusWPF.Service;
using JobPlusWPF.View;
using JobPlusWPF.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

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
            services.AddScoped<UserService>();

            // Регистрация ViewModel
            services.AddScoped<EnterViewModel>();

            // Регистрация окон
            services.AddTransient<EnterWindow>();

            // Строим контейнер зависимостей
            _serviceProvider = services.BuildServiceProvider();

            // Инициализация свойства Services
            Services = _serviceProvider; // Присваиваем свойству значение _serviceProvider
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

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
