using WorkManager.Tokens;

namespace WorkManager.Responses.Interfaces
{
    internal interface IUserResponse
    {
        //public ContainerTokens TokensPair { get; }

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="login">Имя</param>
        /// <param name="password">Пароль</param>
        ContainerTokens Authenticate(string login, string password);

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="login">Имя</param>
        /// <param name="password">Пароль</param>
        void Registration(string login, string password);

        /// <summary>
        /// Обновить токен по токену обновления
        /// </summary>
        /// /// <param name="token">Токен обновления</param>
        string RefreshToken(string token);
    }
}
