using System;

namespace TimeSheets.TimeSheetsExceptions
{
    public class ClientNotFoundException : Exception
    {
        public ClientNotFoundException(string massage) : base(String.Format($"Invalid client id {massage}"))
        {
        }
    }
}
