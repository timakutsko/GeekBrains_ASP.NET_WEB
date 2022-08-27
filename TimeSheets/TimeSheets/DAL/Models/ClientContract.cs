using System;
using TimeSheets.DAL.Interfaces;

namespace TimeSheets.DAL.Models
{
    public class ClientContract : IClientContract, ITSModel
    {
        public string Name { get; private set; }

        public int Id { get; private set; }

        public DateTimeOffset FullTime { get; private set; }

        public ClientContract()
        {
            // create contract must be here....
        }
    }
}
