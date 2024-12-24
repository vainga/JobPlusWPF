using JobPlusWPF.Model.Classes;
using System;

namespace JobPlusWPF.Service
{
    public class CurrentUserService : ICurrentUserService
    {
        private User _currentUser;

        // Метод для установки текущего пользователя после успешного входа
        public void SetCurrentUser(User user)
        {
            _currentUser = user;
        }

        // Метод для получения UserId текущего пользователя
        public int GetCurrentUserId()
        {
            if (_currentUser == null)
            {
                throw new InvalidOperationException("Пользователь не вошел в систему");
            }

            return _currentUser.Id;
        }
    }
}
