using System;

namespace TimeSheets.TimeSheetsExceptions
{
    public class ClientContractNotFoundException : Exception
    {
        public ClientContractNotFoundException(string id) : base(String.Format($"Invalid contract id {id}"))
        {
        }
    }
}
