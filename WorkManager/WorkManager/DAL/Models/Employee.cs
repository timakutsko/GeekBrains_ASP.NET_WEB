using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkManager.DAL.Models.Archive
{
    [Table("ClientContracts", Schema = "WorkManager")]
    public sealed class Employee : PersonEntity
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
