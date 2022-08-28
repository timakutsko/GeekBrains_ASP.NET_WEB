using System;

namespace WorkManager.WorkManagerExceptions
{
    public class ClientContractNotFoundException : Exception
    {
        public ClientContractNotFoundException(string id) : base(String.Format($"Invalid contract id {id}"))
        {
        }
    }
}
