using JobPlusWPF.Model.Classes;
using JobPlusWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JobPlusWPF.View
{
    /// <summary>
    /// Логика взаимодействия для EmplyerDataGrid.xaml
    /// </summary>
    public partial class EmplyerDataGrid : UserControl
    {
        public EmplyerDataGrid(EmplyerDataGridViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
