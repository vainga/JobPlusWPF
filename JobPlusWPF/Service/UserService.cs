using JobPlusWPF.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlusWPF.Service
{
    public class UserService
    {
        private readonly JobPlusWPF.DBLogic.IRepository<User> _userRepository;


        public UserService(JobPlusWPF.DBLogic.IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<User> LoginAsync(string login, string password)
        {
            var existingUser = await _userRepository.FindAsync(u => u.Login == login);
            if (!existingUser.Any())
            {
                throw new InvalidOperationException("User with this login does not exist.");
            }

            var user = existingUser.First();

            if (!user.VerifyPassword(password))
            {
                throw new InvalidOperationException("Invalid password.");
            }

            return user;
        }

        public async Task RegisterUserAsync(string login, string password)
        {
            var existingUser = await _userRepository.FindAsync(u => u.Login == login);
            if (existingUser.Any())
            {
                throw new InvalidOperationException("User with this login already exists.");
            }

            ValidateLogin(login);

            ValidatePassword(password);

            var user = new User(login, password);

            await _userRepository.AddAsync(user);
        } 
     

        private void ValidateLogin(string login)
        {
            // Проверка на наличие пробела
            if (login.Contains(" "))
            {
                throw new InvalidOperationException("Login cannot contain spaces.");
            }

            // Проверка на латинские символы
            if (!login.All(c => char.IsLetterOrDigit(c) || c == '_'))
            {
                throw new InvalidOperationException("Login can only contain Latin letters, digits, and underscores.");
            }
        }

        private void ValidatePassword(string password)
        {
            // Проверка на минимальную длину пароля
            if (password.Length < 8)
            {
                throw new InvalidOperationException("Password must be at least 8 characters long.");
            }

            // Проверка на наличие пробела
            if (password.Contains(" "))
            {
                throw new InvalidOperationException("Password cannot contain spaces.");
            }

            // Проверка на латинские символы
            if (!password.Any(c => char.IsLetter(c) && char.IsLower(c)) || !password.Any(c => char.IsLetter(c) && char.IsUpper(c)))
            {
                throw new InvalidOperationException("Password must contain both uppercase and lowercase Latin letters.");
            }

        }

    }

}
