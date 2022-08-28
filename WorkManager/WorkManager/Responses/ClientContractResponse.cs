using System;
using System.Collections.Generic;
using WorkManager.DAL.Interfaces;
using WorkManager.DAL.Models;
using WorkManager.WorkManagerExceptions;

namespace WorkManager.Responses
{
    public class ClientContractResponse
    {
        /// <summary>
        /// Создание списка всех контрактов в ответ серверу
        /// </summary>
        public IList<ClientContract> GetAllData()
        {
            IList<ClientContract> allElems = new List<ClientContract>();
            return allElems;
        }

        /// <summary>
        /// Передача контракта в ответ серверу
        /// </summary>
        public ClientContract GetById(int id)
        {
            //stub without logic
            ClientContract clientContract = SearchClientContractById(id);
            if (clientContract != null)
            {
                return clientContract;
            }
            else
            {
                throw new ClientContractNotFoundException(id.ToString());
            }
        }

        /// <summary>
        /// Обновление контракта в ответ серверу
        /// </summary>
        public ClientContract UpdateById(int id)
        {
            //stub without logic
            ClientContract clientContract = SearchClientContractById(id);
            if (clientContract != null)
            {
                return clientContract;
            }
            else
            {
                throw new ClientContractNotFoundException(id.ToString());
            }
        }

        /// <summary>
        /// Удаление контракта в ответ серверу
        /// </summary>
        public void DeleteById(int id)
        {
            //stub without logic
        }

        /// <summary>
        /// Создание контракта в ответ серверу
        /// </summary>
        public ClientContract Register()
        {
            ClientContract registerElement = new ClientContract();
            return registerElement;
        }

        private ClientContract SearchClientContractById(int id)
        {
            //stub without logic
            if (id != 1)
            {
                return new ClientContract();
            }
            else
            {
                return null;
            }
        }
    }
}
