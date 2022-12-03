using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using WorkManager.Data.Models;
using WorkManager.Repositories.Interfaces;
using WorkManager.Responses.Interfaces;

namespace WorkManager.Responses
{
    internal sealed class InvoiceResponse : ICrudById<Invoice>, ICrudAllData<Invoice>
    {
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private readonly IRepository<int, Invoice> _repository;

        public InvoiceResponse(IServiceProvider provider)
        {
            _provider = provider;
            _repository = provider.GetService<IRepository<int, Invoice>>();
        }

        public void Register(Invoice entity)
        {
            if (!_repository.Create(entity))
            {
                throw new Exception("Can't create a invoice. Check out input data");
            }
        }

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

        public void UpdateById(int id, string reqColumnName, string value)
        {
            if (!_repository.UpdateById(id, reqColumnName, value))
            {
                throw new Exception($"Client with id: {id} can not update! Maybe it's dosen't exsist or input params are faild?");
            }
        }

        public Invoice GetById(int id)
        {
            Invoice entity = _repository.GetById(id);
            if (entity != null)
            {
                return entity;
            }
            else
            {
                throw new Exception($"Invoice with id: {id} not found. Maybe it's dosen't exsist?");
            }
        }

        public void DeleteById(int id)
        {
            if (!_repository.DeleteById(id))
            {
                throw new Exception($"Invoice with id: {id} can not delete! Maybe it's dosen't exsist?");
            }
        }
    }
}
