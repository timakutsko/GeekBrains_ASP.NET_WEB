using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkManager.Data.Models
{
    [Table("Employees")]
    public sealed class Employee : PersonEntity
    {
        /// <summary>
        /// Размер з.п. сотрудника за 1 час
        /// </summary>
        [Column(TypeName = "money")]
        public int HourSalary { get; set; }

        /// <summary>
        /// Потраченное время сотрудника
        /// </summary>
        [NotMapped]
        public TimeSpan SpendingTime
        {
            get { return TimeSpan.FromTicks(TimeSpanTicks); }
            set { TimeSpanTicks = value.Ticks; }
        }

        [Column(TypeName = "bigint")]
        public long TimeSpanTicks { get; set; }
    }
}
