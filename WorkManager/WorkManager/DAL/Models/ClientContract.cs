using System;
using WorkManager.DAL.Interfaces;

namespace WorkManager.DAL.Models
{
    public class ClientContract : IClientContract
    {
        public int Id { get; private set; }

        public DateTimeOffset FullTime { get; private set; }

        public string Title { get; private set; }

        public ClientContract()
        {
            // create contract must be here....
        }
    }
}
