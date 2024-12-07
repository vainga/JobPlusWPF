using JobPlusWPF.DBLogic;
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
        private AppDbContext _context;

        public App()
        {
            _context = new AppDbContext();
            _context.SeedAdmin();
        }
    }

}
