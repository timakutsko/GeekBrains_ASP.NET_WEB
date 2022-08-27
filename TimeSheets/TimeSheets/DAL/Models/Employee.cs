using System;
using TimeSheets.DAL.Interfaces;

namespace TimeSheets.DAL.Models
{
    public class Employee : IEmployee, ITSModel
    {
        public string Name { get; private set; }

        public int Id { get; private set; }

        public int HourSalary { get; private set; }

        public DateTimeOffset SpendingTime { get; private set; }
    }
}
