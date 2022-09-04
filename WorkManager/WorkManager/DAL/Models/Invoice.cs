using System;
using System.Collections.Generic;
using WorkManager.DAL.Interfaces;

namespace WorkManager.DAL.Models
{
    public class Invoice : WorkResultEntity
    {
        public int Price { get; set; }

        public List<ClientContract> CurrentContracts { get; set; }
    }
}
