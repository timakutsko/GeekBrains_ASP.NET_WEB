using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using WorkManager.DAL.Interfaces;
using WorkManager.DAL.Models;
using WorkManager.Repositories.Interfaces;

namespace WorkManager.Responses
{
    public class EmployeeResponse
    {
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private readonly IRepository<int, Employee> _repository;

        public EmployeeResponse(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IRepository<int, Employee>>();
        }

        /// <summary>
            /// Создание сотрудника в ответ серверу
            /// </summary>
        public void Register(Employee entity)
        {
            if (!_repository.Create(entity))
            {
                throw new Exception("Can't create a employee. Check out input data");
            }
        }

        /// <summary>
        /// Создание списка всех сотрудников в ответ серверу
        /// </summary>
        public IReadOnlyDictionary<int, Employee> GetAllData()
        {
            IReadOnlyDictionary<int, Employee> allElems = _repository.Get();
            if (allElems != null)
            {
                return allElems;
            }
            else
            {
                throw new Exception("Bad request to repo.");
            }
        }

        /// <summary>
        /// Передача сотрудника в ответ серверу
        /// </summary>
        public Employee GetById(int id)
        {
            Employee entity = _repository.GetById(id);
            if (entity != null)
            {
                return entity;
            }
            else
            {
                throw new Exception($"Employee with id: {id} not found. Maybe it's dosen't exsist?");
            }
        }

        /// <summary>
        /// Обновление сотрудника в ответ серверу
        /// </summary>
        public void UpdateById(int id, string reqColumnName, string value)
        {
            if (!_repository.UpdateById(id, reqColumnName, value))
            {
                throw new Exception($"Employee with id: {id} can not update! Maybe it's dosen't exsist or input params are faild?");
            }
        }

        /// <summary>
        /// Удаление сотрудника в ответ серверу
        /// </summary>
        public void DeleteById(int id)
        {
            if (!_repository.DeleteById(id))
            {
                throw new Exception($"Employee with id: {id} can not delete! Maybe it's dosen't exsist?");
            }
        }
    }
}
