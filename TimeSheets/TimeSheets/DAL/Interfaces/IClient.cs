using TimeSheets.DAL.Models;

namespace TimeSheets.DAL.Interfaces
{
    public interface IClient
    {
        /// <summary>
        /// Имя клиента
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Id клиента
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Контракт клиента
        /// </summary>
        public ClientContract  ClientContract { get; }

        /// <summary>
        /// Фирма клиента
        /// </summary>
        public string Company { get; }
    }
}
