using System.Collections.Generic;
using WorkManager.DAL.Interfaces;
using WorkManager.DAL.Models;
using WorkManager.WorkManagerExceptions;

namespace WorkManager.Responses
{
    public class ClientResponse
    {
        /// <summary>
        /// Создание списка всех клиентов в ответ серверу
        /// </summary>
        public IList<Client> GetAllData()
        {
            IList<Client> allElems = new List<Client>();
            return allElems;
        }

        /// <summary>
        /// Передача клиента в ответ серверу
        /// </summary>
        public Client GetById(int id)
        {
            //stub without logic
            Client client = SearchClientContractById(id);
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
        public Client UpdateById(int id)
        {
            //stub without logic
            Client client = SearchClientContractById(id);
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
        public Client Register()
        {
            Client registerElement = new Client();
            return registerElement;
        }

        private Client SearchClientContractById(int id)
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
