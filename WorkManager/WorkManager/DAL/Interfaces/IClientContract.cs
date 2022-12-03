using System;

namespace WorkManager.DAL.Interfaces
{
    public interface IClientContract
    {
        /// <summary>
        /// Id контракта
        /// </summary>
        public int Id { get; }
        
        /// <summary>
        /// Имя контракта
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Общее время на выполнение контратка
        /// </summary>
        public DateTimeOffset FullTime { get; }

        /// <summary>
        /// Проверка на удаление контракта (архивация)
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
