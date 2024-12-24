using JobPlusWPF.Model.Classes;
using JobPlusWPF.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JobPlusWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void jobSeekersDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            // Удаляем автоматически сгенерированный столбец, если он дублирует уже вручную добавленный
            if (e.PropertyName == nameof(JobSeeker.Photo) ||
                e.PropertyName == nameof(JobSeeker.EducationDocumentScan) ||
                e.PropertyName == nameof(JobSeeker.UserId) ||
                e.PropertyName == nameof(JobSeeker.User))
            {
                e.Cancel = true; // Пропускаем генерацию этих столбцов
            }
        }

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}