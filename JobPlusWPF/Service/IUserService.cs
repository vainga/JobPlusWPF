using JobPlusWPF.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlusWPF.Service
{
    public interface IUserService
    {
        Task<User> LoginAsync(string login, string password);
        Task RegisterUserAsync(string login, string password);
    }
}
