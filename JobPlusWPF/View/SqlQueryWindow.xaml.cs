using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using JobPlusWPF.ViewModel;

namespace JobPlusWPF.View
{
    /// <summary>
    /// Логика взаимодействия для SqlQueryWindow.xaml
    /// </summary>
    public partial class SqlQueryWindow : Window
    {
        public SqlQueryWindow(QueryViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var viewModel = (QueryViewModel)DataContext;
            viewModel.SqlQuery = string.Empty;
            viewModel.QueryResult = new DataTable();
        }
    }
}
