using System.ComponentModel.DataAnnotations.Schema;

namespace WorkManager.DAL.Models.Archive
{
    [Table("ClientContracts", Schema = "WorkManager")]
    public sealed class ClientContract : WorkResultEntity
    {

    }
}
