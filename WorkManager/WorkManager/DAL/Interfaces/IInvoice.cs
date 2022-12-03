using System;
using WorkManager.DAL.Models;

namespace WorkManager.DAL.Interfaces
{
    public interface IInvoice
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
