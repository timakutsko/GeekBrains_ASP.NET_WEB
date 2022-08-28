using System;
using WorkManager.DAL.Interfaces;

namespace WorkManager.DAL.Models
{
    public class Employee : IPerson, IEmployee
    {
        public int Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public int Age { get; private set; }

        public int HourSalary { get; private set; }

        public DateTimeOffset SpendingTime { get; private set; }
    }
}
