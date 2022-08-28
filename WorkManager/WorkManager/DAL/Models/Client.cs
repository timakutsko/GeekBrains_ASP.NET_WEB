using System;
using WorkManager.DAL.Interfaces;

namespace WorkManager.DAL.Models
{
    public class Client : IPerson, IClient
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public int Age { get; set; }

        //public ClientContract ClientContract { get; private set; }

        public string Company { get; set; }
    }
}
