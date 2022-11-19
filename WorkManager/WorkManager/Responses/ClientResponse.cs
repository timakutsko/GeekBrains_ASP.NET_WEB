using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using WorkManager.Repositories.Interfaces;
using WorkManager.Data.Models;
using WorkManager.Responses.Interfaces;

namespace WorkManager.Responses
{
    internal sealed class ClientResponse : ICrudById<Client>, ICrudAllData<Client>
    {
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private readonly IRepository<int, Client> _repository;

        public ClientResponse(IServiceProvider provider)
        {
            _provider = provider;
            _repository = provider.GetService<IRepository<int, Client>>();
        }

        public void Register(Client entity)
        {
            if (!_repository.Create(entity))
            {
                throw new Exception("Can't create a person. Check out input data");
            }
        }

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

        public void UpdateById(int id, string reqColumnName, string value)
        {
            if (!_repository.UpdateById(id, reqColumnName, value))
            {
                throw new Exception($"Client with id: {id} can not update! Maybe it's dosen't exsist or input params are faild?");
            }
        }

        public void DeleteById(int id)
        {
            if (!_repository.DeleteById(id))
            {
                throw new Exception($"Client with id: {id} can not delete! Maybe it's dosen't exsist?");
            }
        }
    }
}
