using System.Collections.Generic;

namespace WorkManager.DAL.Models.Archive
{
    public sealed class Invoice : WorkResultEntity
    {
        public int Price { get; set; }

        public List<ClientContract> CurrentContracts { get; set; }
    }
}
