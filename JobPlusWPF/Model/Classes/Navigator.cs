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


        public void CloseCurrentWindow()
        {
            Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)?.Close();
        }
    }

}
