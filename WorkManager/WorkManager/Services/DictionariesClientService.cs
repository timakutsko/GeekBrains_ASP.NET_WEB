using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkManager.Repositories.Interfaces;
using WorkManagerProto;
using static WorkManagerProto.DictionariesClient;

namespace WorkManager.Services
{
    public class DictionariesClientService : DictionariesClientBase
    {

        private readonly IServiceProvider _provider;
        private readonly IRepository<int, Data.Models.Client> _repository;

        public DictionariesClientService(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IRepository<int, Data.Models.Client>>();
        }

        public override Task<GetElementsResponse> GetElements(GetElementsRequest request, ServerCallContext context)
        {
            GetElementsResponse response = new GetElementsResponse();
            response.Clients.AddRange(_repository.Get().Select(e =>
                new Client
                {
                    Id = e.Value.Id,
                    FirstName = e.Value.FirstName,
                    LastName = e.Value.LastName,
                    Email = e.Value.Email,
                    Age = e.Value.Age,
                    Company = e.Value.Company
                }).ToList());

            return Task.FromResult(response);
        }

        public override Task<GetByIdResponse> GetById(GetByIdRequest request, ServerCallContext context)
        {
            GetByIdResponse response = new GetByIdResponse();
            var dbCllient = _repository.GetById(request.Id);
            response.Client = new Client
            {
                Id = dbCllient.Id,
                FirstName = dbCllient.FirstName,
                LastName = dbCllient.LastName,
                Email = dbCllient.Email,
                Age = dbCllient.Age,
                Company = dbCllient.Company
            };

            return Task.FromResult(response);
        }

        public override Task<DeleteByIdResponse> DeleteById(DeleteByIdIdRequest request, ServerCallContext context)
        {
            _repository.DeleteById(request.Id);

            return Task.FromResult(new DeleteByIdResponse());
        }
    }
}
