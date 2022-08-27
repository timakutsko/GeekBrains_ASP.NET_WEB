using System;

namespace TimeSheets.DAL.Interfaces
{
    public interface IClientContract
    {
        /// <summary>
        /// Id контракта
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Общее время на выполнение контратка
        /// </summary>
        public DateTimeOffset FullTime { get; }
    }
}
