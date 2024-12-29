using System;
using System.Collections.Generic;
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
                QueryResult = await ExecuteSqlQuery(SqlQuery);
            }
            catch (Exception ex)
            {
                // Handle any query errors
                Console.WriteLine($"Error executing query: {ex.Message}");
            }
        }

        private async Task<DataTable> ExecuteSqlQuery(string query)
        {
            var result = new DataTable();

            using (var connection = _context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = new NpgsqlCommand(query, (NpgsqlConnection)connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    result.Load(reader);
                }
            }

            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
