using System.Collections.Generic;
using WorkManager.DAL.Interfaces;
using WorkManager.DAL.Models;

namespace WorkManager.Responses
{
    public class InvoiceResponse
    {
        /// <summary>
        /// Создание списка всех счетов в ответ серверу
        /// </summary>
        public IList<Invoice> GetAllData()
        {
            IList<Invoice> allElems = new List<Invoice>();
            return allElems;
        }

        /// <summary>
        /// Обновление всех счетов в ответ серверу
        /// </summary>
        public void UpdateAllData()
        {
            // some logic
        }
    }
}
