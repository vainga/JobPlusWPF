using JobPlusWPF.DBLogic;
using JobPlusWPF.Model.Classes;
using JobPlusWPF.Service;
using JobPlusWPF.View;
using JobPlusWPF.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace JobPlusWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    public partial class App : Application
    {
        //private ServiceProvider _serviceProvider;

        private AppDbContext _context;

        public App()
        {
            //ConfigureServices();
            //SeedDatabase();
            _context = new AppDbContext();
            _context.SeedAdmin();
        }

        //private void ConfigureServices()
        //{
        //    var services = new ServiceCollection();

        //    // Регистрация DbContext
        //    services.AddDbContext<AppDbContext>();

        //    // Регистрация репозиториев и сервисов
        //    services.AddScoped<IRepository<User>, Repository<User>>();
        //    services.AddScoped<UserService>();

        //    // Регистрация ViewModel
        //    services.AddScoped<EnterViewModel>();

        //    // Регистрация окон
        //    services.AddTransient<EnterWindow>();

        //    _serviceProvider = services.BuildServiceProvider();
        //}

        //private void SeedDatabase()
        //{
        //    // Инициализация базы данных
        //    using var scope = _serviceProvider.CreateScope();
        //    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        //    context.SeedAdmin();
        //}

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);

        //    // Получаем экземпляр EnterViewModel
        //    var enterViewModel = _serviceProvider.GetRequiredService<EnterViewModel>();

        //    // Создаем окно и передаем ViewModel в конструктор
        //    var enterWindow = _serviceProvider.GetRequiredService<EnterWindow>();
        //    enterWindow.DataContext = enterViewModel; // Обязательно установите DataContext
        //    enterWindow.Show();
        //}


        //protected override void OnExit(ExitEventArgs e)
        //{
        //    base.OnExit(e);

        //    if (_serviceProvider is IDisposable disposable)
        //    {
        //        disposable.Dispose();
        //    }
        //}
    }

}
