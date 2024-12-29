using JobPlusWPF.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace JobPlusWPF.View
{
    /// <summary>
    /// Логика взаимодействия для JobSeekerAddWindow.xaml
    /// </summary>
    public partial class JobSeekerAddWindow : Window
    {

        public JobSeekerAddWindow(JobSeekerAddViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

        }

        //private void Window_Closed(object sender, EventArgs e)
        //{
        //    // Очистка всех полей в ViewModel
        //    var viewModel = DataContext as JobSeekerAddViewModel;
        //    if (viewModel != null)
        //    {
        //        // Сбрасываем простые типы данных
        //        viewModel.Name = string.Empty;
        //        viewModel.Surname = string.Empty;
        //        viewModel.Age = 0;
        //        viewModel.PassportNumber = string.Empty;
        //        viewModel.PassportIssueDate = DateTime.Now;
        //        viewModel.PassportIssuedBy = string.Empty;
        //        viewModel.StreetName = string.Empty;
        //        viewModel.CityName = string.Empty;
        //        viewModel.Phone = string.Empty;
        //        viewModel.Institution = string.Empty;
        //        viewModel.Specialty = string.Empty;
        //        viewModel.WorkExperienceYears = 0;
        //        viewModel.WorkExperienceMonths = 0;
        //        viewModel.EducationDocumentFileName = string.Empty;
        //        viewModel.Photo = null;

        //        // Сбрасываем коллекции и сложные объекты
        //        viewModel.Statuses = null;  // если есть коллекция или объект статусов
        //        viewModel.Vacancies = null; // если есть коллекция вакансий
        //        viewModel.SelectedStatus = null; // если есть выбранный статус
        //        viewModel.SelectedVacancy = null; // если есть выбранная вакансия
        //        viewModel.City = null; // если есть объект города
        //        viewModel.Street = null; // если есть объект улицы
        //    }
        //}
    }
}
