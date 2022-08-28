using System;
using WorkManager.DAL.Interfaces;

namespace WorkManager.DAL.Models
{
    public class Invoice : IInvoice
    {
        public int Id { get; private set; }

        public int Price { get; private set; }

        public ClientContract CurrentContract { get; private set; }
    }
}
