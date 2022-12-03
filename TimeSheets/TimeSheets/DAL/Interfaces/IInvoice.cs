using System;

namespace TimeSheets.DAL.Interfaces
{
    public interface IInvoice
    {
        /// <summary>
        /// Id счета
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Размер счета
        /// </summary>
        public int Price { get; }
    }
}
