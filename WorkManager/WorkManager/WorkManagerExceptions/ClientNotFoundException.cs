using System;

namespace WorkManager.WorkManagerExceptions
{
    public class ClientNotFoundException : Exception
    {
        public ClientNotFoundException(string massage) : base(String.Format($"Invalid client id {massage}"))
        {
        }
    }
}
