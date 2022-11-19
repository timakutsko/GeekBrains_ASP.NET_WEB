using System;

namespace WorkManager.DAL.Interfaces.Archive
{
    public interface IPerson
    {
        /// <summary>
        /// Уникальный идентификатор персоны
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Имя персоны
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Фамилия персоны
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// E-mail персоны
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Возраст персоны
        /// </summary>
        public int Age { get; }

        /// <summary>
        /// Проверка на удаление персоны (архивация)
        /// </summary>
        public bool IsDeleted { get; }
    }
}
