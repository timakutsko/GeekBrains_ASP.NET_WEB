namespace WorkManager.DAL.Models
{
    public class PersonEntity
    {
        /// <summary>
        /// Уникальный идентификатор персоны
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя персоны
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия персоны
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// E-mail персоны
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Возраст персоны
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Проверка на удаление персоны (архивация)
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
