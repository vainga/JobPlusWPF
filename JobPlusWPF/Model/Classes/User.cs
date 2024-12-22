using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Role
{
    User,
    Admin
}

namespace JobPlusWPF.Model.Classes
{
    public class User
    {
        public int Id { get; private set; }
        public string Login { get; private set; }
        private string Password { get;  set; }
        public Role Role { get; private set; }

        private string salt = BCrypt.Net.BCrypt.GenerateSalt(6);

        public User(string login, string password, Role role = Role.User)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(6);
            Login = login;
            Password = BCrypt.Net.BCrypt.HashPassword(password, salt);
            Role = role;
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }

        private void UpdatePassword(string newPassword)
        {
            Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
        }

        private void UpdateRole(Role newRole)
        {
            Role = newRole;
        }

    }
}
