using System;
using System.ComponentModel.DataAnnotations.Schema;
using WorkManager.DAL.Interfaces;

namespace WorkManager.DAL.Models
{
    [Table("ClientContracts", Schema = "WorkManager")]
    public class ClientContract : WorkResultEntity
    {
        
    }
}
