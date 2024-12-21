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
        public string Password { get; private set; }
        public Role Role { get; private set; }

        public User(string login, string password, Role role = Role.User)
        {
            Login = login;
            Password = BCrypt.Net.BCrypt.HashPassword(password);
            Role = role;
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }

        public void UpdatePassword(string newPassword)
        {
            Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
        }

        public void UpdateRole(Role newRole)
        {
            Role = newRole;
        }


    }
}
