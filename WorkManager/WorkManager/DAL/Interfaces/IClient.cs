using WorkManager.DAL.Models;

namespace WorkManager.DAL.Interfaces.Archive
{
    public interface IClient
    {
        /// <summary>
        /// Контракт клиента
        /// </summary>
        //public ClientContract  ClientContract { get; }

        /// <summary>
        /// Фирма клиента
        /// </summary>
        public string Company { get; }
    }
}
