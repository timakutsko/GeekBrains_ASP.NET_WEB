using System;

namespace TimeSheets.DAL.Interfaces
{
    public interface IEmployee
    {
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Id сотрудника
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Размер з.п. сотрудника за 1 час
        /// </summary>
        public int HourSalary { get; }

        /// <summary>
        /// Потраченное время сотрудника
        /// </summary>
        public DateTimeOffset SpendingTime { get; }
    }
}
