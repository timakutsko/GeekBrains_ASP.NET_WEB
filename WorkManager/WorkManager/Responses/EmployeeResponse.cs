using System.Collections.Generic;
using WorkManager.DAL.Interfaces;
using WorkManager.DAL.Models;
using WorkManager.WorkManagerExceptions;

namespace WorkManager.Responses
{
    public class EmployeeResponse
    {
        /// <summary>
        /// Создание списка всех сотрудников в ответ серверу
        /// </summary>
        public IList<Employee> GetAllData()
        {
            IList<Employee> allElems = new List<Employee>();
            return allElems;
        }

        /// <summary>
        /// Передача сотрудника в ответ серверу
        /// </summary>
        public Employee GetById(int id)
        {
            //stub without logic
            Employee client = SearchClientContractById(id);
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
        public Employee UpdateById(int id)
        {
            //stub without logic
            Employee client = SearchClientContractById(id);
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
        public Employee Register()
        {
            Employee registerElement = new Employee();
            return registerElement;
        }

        private Employee SearchClientContractById(int id)
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
