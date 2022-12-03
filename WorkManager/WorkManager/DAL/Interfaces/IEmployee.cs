using System;

namespace WorkManager.DAL.Interfaces
{
    public interface IEmployee
    {
        /// <summary>
        /// Размер з.п. сотрудника за 1 час
        /// </summary>
        public int HourSalary { get; }

        /// <summary>
        /// Потраченное время сотрудника
        /// </summary>
        public TimeSpan SpendingTime { get; }
    }
}
