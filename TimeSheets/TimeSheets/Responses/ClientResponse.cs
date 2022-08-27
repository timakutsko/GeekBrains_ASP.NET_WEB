using System.Collections.Generic;
using TimeSheets.DAL.Interfaces;
using TimeSheets.DAL.Models;
using TimeSheets.Responses.Interfaces;
using TimeSheets.TimeSheetsExceptions;

namespace TimeSheets.Responses
{
    public class ClientResponse : IWorkByIdResponse, IRegisterResponse, IGetAllDataResponse
    {
        /// <summary>
        /// Создание списка всех клиентов в ответ серверу
        /// </summary>
        public IList<ITSModel> GetAllData()
        {
            IList<ITSModel> allElems = new List<ITSModel>();
            return allElems;
        }

        /// <summary>
        /// Передача клиента в ответ серверу
        /// </summary>
        public ITSModel GetById(int id)
        {
            //stub without logic
            Client client = SearchClientContractById(id) as Client;
            if (client != null)
            {
                return client;
            }
            else
            {
                throw new ClientNotFoundException(id.ToString());
            }
        }

        /// <summary>
        /// Обновление клиента в ответ серверу
        /// </summary>
        public ITSModel UpdateById(int id)
        {
            //stub without logic
            Client client = SearchClientContractById(id) as Client;
            if (client != null)
            {
                return client;
            }
            else
            {
                throw new ClientNotFoundException(id.ToString());
            }
        }

        /// <summary>
        /// Удаление клиента в ответ серверу
        /// </summary>
        public void DeleteById(int id)
        {
            //stub without logic
        }

        /// <summary>
        /// Создание клиента в ответ серверу
        /// </summary>
        public ITSModel Register()
        {
            ITSModel registerElement = new Client();
            return registerElement;
        }

        private ITSModel SearchClientContractById(int id)
        {
            //stub without logic
            if (id != 1)
            {
                return new Client();
            }
            else
            {
                return null;
            }
        }
    }
}
