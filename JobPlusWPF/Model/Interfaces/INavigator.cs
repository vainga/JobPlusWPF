using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JobPlusWPF.Model.Interfaces
{
    public interface INavigator
    {
        void NavigateTo<TWindow>() where TWindow : Window;
        void CloseCurrentWindow();
    }
}
