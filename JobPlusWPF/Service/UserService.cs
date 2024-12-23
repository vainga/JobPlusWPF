using JobPlusWPF.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlusWPF.Service
{
    public class UserService : IUserService
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
                throw new InvalidOperationException("Пользователь с этим логином не существует");
            }

            var user = existingUser.First();

            var storedPasswordHash = user.Password;

            if (!user.VerifyPassword(password))
            {
                throw new InvalidOperationException("Неверный пароль");
            }

            return user;
        }

        public async Task RegisterUserAsync(string login, string password)
        {
            var existingUser = await _userRepository.FindAsync(u => u.Login == login);
            if (existingUser.Any())
            {
                throw new InvalidOperationException("Пользователь с этим логином уже существует");
            }

            ValidateLogin(login);

            ValidatePassword(password);

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User(login, hashedPassword);

            await _userRepository.AddAsync(user);
        } 
     

        private void ValidateLogin(string login)
        {
            // Проверка на наличие пробела
            if (login.Contains(" "))
            {
                throw new InvalidOperationException("Логин не должен содержать пробелы");
            }

            // Проверка на латинские символы
            if (!login.All(c => char.IsLetterOrDigit(c) || c == '_'))
            {
                throw new InvalidOperationException("Логин может содержать только латинские буквы, цифры и символы подчеркивания");
            }
        }

        private void ValidatePassword(string password)
        {
            // Проверка на минимальную длину пароля
            //if (password.Length < 8)
            //{
            //    throw new InvalidOperationException("Длина пароля должна составлять не менее 8 символов");
            //}

            // Проверка на наличие пробела
            if (password.Contains(" "))
            {
                throw new InvalidOperationException("Пароль не должен содержать пробелов");
            }

            // Проверка на латинские символы
            //if (!password.Any(c => char.IsLetter(c) && char.IsLower(c)) || !password.Any(c => char.IsLetter(c) && char.IsUpper(c)))
            //{
            //    throw new InvalidOperationException("Пароль должен содержать как заглавные, так и строчные латинские буквы");
            //}

        }

    }

}
