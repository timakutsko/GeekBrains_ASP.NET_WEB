using TimeSheets.DAL.Interfaces;

namespace TimeSheets.DAL.Models
{
    public class Client : IClient, ITSModel
    {
        public string Name { get; private set; }

        public int Id { get; private set; }

        public ClientContract ClientContract { get; private set; }

        public string Company { get; private set; }
    }
}
