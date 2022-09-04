using System;
using WorkManager.DAL.Interfaces;

namespace WorkManager.DAL.Models
{
    public class Employee : PersonEntity
    {
        /// <summary>
        /// Размер з.п. сотрудника за 1 час
        /// </summary>
        public int HourSalary { get; set; }

        /// <summary>
        /// Потраченное время сотрудника
        /// </summary>
        public TimeSpan SpendingTime { get; set; }
    }
}
