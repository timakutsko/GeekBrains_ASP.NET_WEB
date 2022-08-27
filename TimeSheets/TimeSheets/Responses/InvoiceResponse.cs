using System.Collections.Generic;
using TimeSheets.DAL.Interfaces;
using TimeSheets.DAL.Models;
using TimeSheets.Responses.Interfaces;

namespace TimeSheets.Responses
{
    public class InvoiceResponse : IGetAllDataResponse
    {
        /// <summary>
        /// Создание списка всех счетов в ответ серверу
        /// </summary>
        public IList<ITSModel> GetAllData()
        {
            IList<ITSModel> allElems = new List<ITSModel>();
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
