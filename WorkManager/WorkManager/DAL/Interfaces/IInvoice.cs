using System;
using WorkManager.DAL.Models.Archive;

namespace WorkManager.DAL.Interfaces.Archive
{
    internal interface IInvoice
    {
        /// <summary>
        /// Id счета
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Контракт, по которому выставлен счет
        /// </summary>
        public ClientContract CurrentContract { get; }
        
        /// <summary>
        /// Размер счета
        /// </summary>
        public int Price { get; }
    }
}
