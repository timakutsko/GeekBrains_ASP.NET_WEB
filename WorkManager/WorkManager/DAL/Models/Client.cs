using System;
using System.ComponentModel.DataAnnotations.Schema;
using WorkManager.DAL.Interfaces;

namespace WorkManager.DAL.Models
{
    [Table("Clients", Schema = "WorkManager")]
    public class Client : PersonEntity
    {
        //public ClientContract ClientContract { get; private set; }

        public string Company { get; set; }
    }
}
