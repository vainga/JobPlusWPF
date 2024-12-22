using JobPlusWPF.Model.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JobPlusWPF.Model.Classes
{
    public class Navigator : INavigator
    {
        private readonly IServiceProvider _serviceProvider;

        public Navigator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<TWindow>() where TWindow : Window
        {
            // Закрываем текущее активное окно
            Application.Current.MainWindow?.Close();

            // Получаем окно через DI
            var window = _serviceProvider.GetRequiredService<TWindow>();

            // Устанавливаем его как главное окно и показываем
            Application.Current.MainWindow = window;
            window.Show();
        }

        public void CloseCurrentWindow()
        {
            Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)?.Close();
        }
    }

}
