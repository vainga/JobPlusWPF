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

        public User(int id, string login, string password, Role role = Role.User)
        {
            Id = id;
            Login = login;
            Password = BCrypt.Net.BCrypt.HashPassword(password);
            Role = role;
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }

    }
}
