using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using WorkManager.DAL.Interfaces;
using WorkManager.DAL.Models;
using WorkManager.Repositories.Interfaces;

namespace WorkManager.Responses
{
    public class ClientContractResponse
    {
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private readonly IRepository<int, ClientContract> _repository;

        public ClientContractResponse(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IRepository<int, ClientContract>>();
        }

        /// <summary>
        /// Создание контракта в ответ серверу
        /// </summary>
        public void Register(ClientContract entity)
        {
            if (!_repository.Create(entity))
            {
                throw new Exception("Can't create a contract. Check out input data");
            }
        }

        /// <summary>
        /// Создание списка всех контрактов в ответ серверу
        /// </summary>
        public IReadOnlyDictionary<int, ClientContract> GetAllData()
        {
            IReadOnlyDictionary<int, ClientContract> allElems = _repository.Get();
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
        /// Передача контракта в ответ серверу
        /// </summary>
        public ClientContract GetById(int id)
        {
            ClientContract entity = _repository.GetById(id);
            if (entity != null)
            {
                return entity;
            }
            else
            {
                throw new Exception($"Contract with id: {id} not found. Maybe it's dosen't exsist?");
            }
        }

        /// <summary>
        /// Обновление контракта в ответ серверу
        /// </summary>
        public void UpdateById(int id, string reqColumnName, string value)
        {
            if (!_repository.UpdateById(id, reqColumnName, value))
            {
                throw new Exception($"Client with id: {id} can not update! Maybe it's dosen't exsist or input params are faild?");
            }
        }

        /// <summary>
        /// Удаление контракта в ответ серверу
        /// </summary>
        public void DeleteById(int id)
        {
            if (!_repository.DeleteById(id))
            {
                throw new Exception($"Contract with id: {id} can not delete! Maybe it's dosen't exsist?");
            }
        }
    }
}
