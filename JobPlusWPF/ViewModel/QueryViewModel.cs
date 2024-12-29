using System;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Input;
using JobPlusWPF.DBLogic;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace JobPlusWPF.ViewModel
{
    public class QueryViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context;
        private string _sqlQuery;
        private DataTable _queryResult;

        public QueryViewModel(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            ExecuteQueryCommand = new RelayCommand(ExecuteQuery);
        }

        public string SqlQuery
        {
            get => _sqlQuery;
            set
            {
                _sqlQuery = value;
                OnPropertyChanged(nameof(SqlQuery));
            }
        }

        public DataTable QueryResult
        {
            get => _queryResult;
            set
            {
                _queryResult = value;
                OnPropertyChanged(nameof(QueryResult));
            }
        }

        public ICommand ExecuteQueryCommand { get; }

        private async void ExecuteQuery(object parameter)
        {
            if (string.IsNullOrWhiteSpace(SqlQuery))
            {
                return;
            }

            try
            {
                // Сброс результата перед выполнением нового запроса
                QueryResult = null;

                // Выполнение запроса
                var result = await ExecuteSqlQuery(SqlQuery);
                QueryResult = result;
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                Console.WriteLine($"Error executing query: {ex.Message}");
            }
        }

        private async Task<DataTable> ExecuteSqlQuery(string query)
        {
            var result = new DataTable();

            try
            {
                await using (var connection = new NpgsqlConnection(_context.Database.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    await using (var command = new NpgsqlCommand(query, connection))
                    {
                        await using (var reader = await command.ExecuteReaderAsync())
                        {
                            result.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
            }

            return result;
        }

        public void ClearData()
        {
            SqlQuery = string.Empty;
            QueryResult = null; // Сброс результата запроса
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
