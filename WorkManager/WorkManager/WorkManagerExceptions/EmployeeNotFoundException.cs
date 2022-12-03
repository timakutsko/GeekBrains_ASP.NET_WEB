using System;

namespace WorkManager.WorkManagerExceptions
{
    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException(string massage) : base(String.Format($"Invalid employee id {massage}"))
        {
        }
    }
}
