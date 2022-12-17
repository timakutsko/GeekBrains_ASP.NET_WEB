using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WorkManager.Repositories.Interfaces;
using WorkManager.Data.Contexts;
using WorkManager.Data.Models;

namespace WorkManager.Repositories
{
    public class ClientsRepository : IRepository<int, Client>
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly IDbContext _context;

        public ClientsRepository(IDbContext dbContext)
        {
            _context = dbContext;
        }

        public bool Create(Client entity)
        {
            _context.Clients.Add(entity);
            _context.SaveChanges();
            return true;
        }

        public IReadOnlyDictionary<int, Client> Get()
        {
            // Подгружаю элементы из БД
            IEnumerable<Client> entityColl = _context
                .Clients
                .Where(c => c.IsDeleted == false)
                .AsEnumerable();

            Dictionary<int, Client> entitysIndex = entityColl
                .ToDictionary(c => c.Id, c => c);

            IReadOnlyDictionary<int, Client> result = entitysIndex;
            return result;
        }

        public Client GetById(int id)
        {
            return _context.Clients.SingleOrDefault(c => c.Id == id);
        }

        public bool UpdateById(int id, string reqColumnName, string value)
        {
            Client entity = GetById(id);
            if (entity != null)
            {
                PropertyInfo prop = entity.GetType().GetProperty(reqColumnName, BindingFlags.Public | BindingFlags.Instance);
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(entity, value, null);
                    _context.Update(entity);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }

            return false;
        }

        public bool DeleteById(int id)
        {
            Client entity = GetById(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                _context.Update(entity);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
