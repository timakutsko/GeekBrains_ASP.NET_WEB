using WorkManager.Models;
using WorkManager.Tokens;

namespace WorkManager.Responses.Interfaces
{
    internal interface IAccountResponse
    {
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="login">Имя</param>
        /// <param name="password">Пароль</param>
        void Registration(string login, string password);

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="login">Имя</param>
        /// <param name="password">Пароль</param>
        AuthenticateDto Authenticate(string login, string password);

        /// <summary>
        /// Текущая сессия
        /// </summary>
        /// /// <param name="token">Токен обновления</param>
        SessionDto GetSession(string token);

        /// <summary>
        /// Обновить токен по токену обновления
        /// </summary>
        /// /// <param name="token">Токен обновления</param>
        string RefreshToken(string token);
    }
}
