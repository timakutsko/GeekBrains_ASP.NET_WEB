using System;
using System.Collections.Generic;
using TimeSheets.DAL.Interfaces;
using TimeSheets.DAL.Models;
using TimeSheets.Responses.Interfaces;
using TimeSheets.TimeSheetsExceptions;

namespace TimeSheets.Responses
{
    public class ClientContractResponse : IWorkByIdResponse, IRegisterResponse, IGetAllDataResponse
    {
        /// <summary>
        /// Создание списка всех контрактов в ответ серверу
        /// </summary>
        public IList<ITSModel> GetAllData()
        {
            IList<ITSModel> allElems = new List<ITSModel>();
            return allElems;
        }

        /// <summary>
        /// Передача контракта в ответ серверу
        /// </summary>
        public ITSModel GetById(int id)
        {
            //stub without logic
            ClientContract clientContract = SearchClientContractById(id) as ClientContract;
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
        public ITSModel UpdateById(int id)
        {
            //stub without logic
            ClientContract clientContract = SearchClientContractById(id) as ClientContract;
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
        public ITSModel Register()
        {
            ITSModel registerElement = new ClientContract();
            return registerElement;
        }

        private ITSModel SearchClientContractById(int id)
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
