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
            var previousWindow = Application.Current.MainWindow;

            var window = _serviceProvider.GetRequiredService<TWindow>();

            Application.Current.MainWindow = window;

            window.Show();

            previousWindow?.Close();
        }

        public void ShowDialog<T>() where T : Window
        {
            var window = _serviceProvider.GetService<T>();
            if (window != null)
            {
                window.Owner = Application.Current.MainWindow; // Установка владельца
                window.ShowDialog(); // Открытие окна в модальном режиме
            }
        }

        public void CloseCurrentWindow()
        {
            Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)?.Close();
        }

        public void CloseWindow<T>() where T : Window
        {
            var window = Application.Current.Windows.OfType<T>().FirstOrDefault();
            window?.Close();
        }

    }

}
