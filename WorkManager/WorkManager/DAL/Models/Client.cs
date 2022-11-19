using System.ComponentModel.DataAnnotations.Schema;

namespace WorkManager.DAL.Models.Archive
{
    [Table("Clients", Schema = "WorkManager_v2")]
    public sealed class Client : PersonEntity
    {
        //public ClientContract ClientContract { get; private set; }

        public string Company { get; set; }
    }
}
