using System.ComponentModel.DataAnnotations.Schema;

namespace WorkManager.Data.Models
{
    [Table("Clients")]
    public sealed class Client : PersonEntity
    {
        //public ClientContract ClientContract { get; private set; }

        [Column(TypeName = "nvarchar(128)")]
        public string? Company { get; set; }
    }
}
