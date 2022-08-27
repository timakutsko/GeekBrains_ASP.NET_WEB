using TimeSheets.DAL.Interfaces;

namespace TimeSheets.DAL.Models
{
    public class Invoice : IInvoice, ITSModel
    {
        public string Name { get; private set; }

        public int Id { get; private set; }

        public int Price { get; private set; }
    }
}
