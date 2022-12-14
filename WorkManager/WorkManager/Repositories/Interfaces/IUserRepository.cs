using System.Collections.Generic;
using WorkManager.Data.Models;

namespace WorkManager.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Записывает пользователя в БД
        /// </summary>
        /// <param name="entity">Элемент для записи</param>
        bool Create(Account entity);

        /// <summary>
        /// Возвращает всех пользователей из БД
        /// </summary>
        /// <returns>Коллекция элементов</returns>
        IReadOnlyDictionary<int, Account> Get();

        /// <summary>
		/// Записывает токен обновления в БД
		/// </summary>
		/// <param name="login">Логин пользователь для обновления</param>
		/// <param name="passwordHash">Хэш пароля пользователя для обновления</param>
		/// <param name="refreshToken">Токен для записи</param>
        bool SetRefreshToken(string login, string passwordHash, string refreshToken);
    }
}
