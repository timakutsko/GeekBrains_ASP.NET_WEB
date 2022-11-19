using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkManager.Data.Models
{
    [Table("Invoices")]
    public sealed class Invoice : WorkResultEntity
    {
        [Column(TypeName = "money")]
        public int Price { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public List<ClientContract>? CurrentContractIds { get; set; }
    }
}
