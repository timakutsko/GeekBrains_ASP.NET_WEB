using System;

namespace WorkManager.DAL.Models.Archive
{
    public class WorkResultEntity
    {
        /// <summary>
        /// Уникальный идентификатор работы
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя работы
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Общее время в часах на выполнение работы
        /// </summary>
        public TimeSpan FullTime { get; set; }

        /// <summary>
        /// Проверка на удаление работы (архивация)
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
