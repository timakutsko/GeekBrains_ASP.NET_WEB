using System.Collections.Generic;
using TimeSheets.DAL.Interfaces;
using TimeSheets.DAL.Models;
using TimeSheets.Responses.Interfaces;
using TimeSheets.TimeSheetsExceptions;

namespace TimeSheets.Responses
{
    public class EmployeeResponse : IWorkByIdResponse, IRegisterResponse, IGetAllDataResponse
    {
        /// <summary>
        /// Создание списка всех сотрудников в ответ серверу
        /// </summary>
        public IList<ITSModel> GetAllData()
        {
            IList<ITSModel> allElems = new List<ITSModel>();
            return allElems;
        }

        /// <summary>
        /// Передача сотрудника в ответ серверу
        /// </summary>
        public ITSModel GetById(int id)
        {
            //stub without logic
            Employee client = SearchClientContractById(id) as Employee;
            if (client != null)
            {
                return client;
            }
            else
            {
                throw new EmployeeNotFoundException(id.ToString());
            }
        }

        /// <summary>
        /// Обновление сотрудника в ответ серверу
        /// </summary>
        public ITSModel UpdateById(int id)
        {
            //stub without logic
            Employee client = SearchClientContractById(id) as Employee;
            if (client != null)
            {
                return client;
            }
            else
            {
                throw new EmployeeNotFoundException(id.ToString());
            }
        }

        /// <summary>
        /// Удаление сотрудника в ответ серверу
        /// </summary>
        public void DeleteById(int id)
        {
            //stub without logic
        }

        /// <summary>
        /// Создание сотрудника в ответ серверу
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
                return new Employee();
            }
            else
            {
                return null;
            }
        }
    }
}
