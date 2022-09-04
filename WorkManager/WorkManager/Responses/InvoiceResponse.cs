using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using WorkManager.DAL.Interfaces;
using WorkManager.DAL.Models;
using WorkManager.Repositories.Interfaces;

namespace WorkManager.Responses
{
    public class InvoiceResponse
    {
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private readonly IRepository<int, Invoice> _repository;

        public InvoiceResponse(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IRepository<int, Invoice>>();
        }

        /// <summary>
        /// Создание счета в ответ серверу
        /// </summary>
        public void Register(Invoice entity)
        {
            if (!_repository.Create(entity))
            {
                throw new Exception("Can't create a invoice. Check out input data");
            }
        }

        /// <summary>
        /// Создание списка всех счетов в ответ серверу
        /// </summary>
        public IReadOnlyDictionary<int, Invoice> GetAllData()
        {
            IReadOnlyDictionary<int, Invoice> allElems = _repository.Get();
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
        /// Обновление счета по id в ответ серверу
        /// </summary>
        public void UpdateById(int id, string reqColumnName, string value)
        {
            if (!_repository.UpdateById(id, reqColumnName, value))
            {
                throw new Exception($"Client with id: {id} can not update! Maybe it's dosen't exsist or input params are faild?");
            }
        }
    }
}
