using WorkManager.Models;
using WorkManager.Requests;
using WorkManager.Tokens;

namespace WorkManager.Responses.Interfaces
{
    public interface IAccountResponse
    {
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="auth">Запрос</param>
        void Registration(AuthenticateRequest auth);

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="auth">Запрос</param>
        AuthenticateDto Authenticate(AuthenticateRequest auth);

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
