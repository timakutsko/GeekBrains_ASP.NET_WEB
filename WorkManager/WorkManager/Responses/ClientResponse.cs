using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using WorkManager.DAL.Interfaces;
using WorkManager.DAL.Models;
using WorkManager.Repositories.Interfaces;

namespace WorkManager.Responses
{
    public class ClientResponse
    {
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private readonly IRepository<int, Client> _repository;

        public ClientResponse(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IRepository<int, Client>>();
        }

        /// <summary>
        /// Создание клиента в ответ серверу
        /// </summary>
        public void Register(Client entity)
        {
            if (!_repository.Create(entity))
            {
                throw new Exception("Can't create a person. Check out input data");
            }
        }

        /// <summary>
        /// Создание списка всех клиентов в ответ серверу
        /// </summary>
        public IReadOnlyDictionary<int, Client> GetAllData()
        {
            IReadOnlyDictionary<int, Client> allElems = _repository.Get();
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
        /// Передача клиента в ответ серверу
        /// </summary>
        public Client GetById(int id)
        {
            Client entity = _repository.GetById(id);
            if (entity != null)
            {
                return entity;
            }
            else
            {
                throw new Exception($"Client with id: {id} not found. Maybe it's dosen't exsist?");
            }
        }

        /// <summary>
        /// Обновление клиента в ответ серверу
        /// </summary>
        public void UpdateById(int id, string reqColumnName, string value)
        {
            if (!_repository.UpdateById(id, reqColumnName, value))
            {
                throw new Exception($"Client with id: {id} can not update! Maybe it's dosen't exsist or input params are faild?");
            }
        }

        /// <summary>
        /// Удаление клиента в ответ серверу
        /// </summary>
        public void DeleteById(int id)
        {
            if (!_repository.DeleteById(id))
            {
                throw new Exception($"Client with id: {id} can not delete! Maybe it's dosen't exsist?");
            }
        }
    }
}
