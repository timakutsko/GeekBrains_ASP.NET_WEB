using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WorkManager.Repositories.Interfaces;
using WorkManager.Data.Contexts;
using WorkManager.Data.Models;

namespace WorkManager.Repositories
{
    internal sealed class InvoicesRepository : IRepository<int, Invoice>
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly WorkManagerDbContext _context;

        public InvoicesRepository(WorkManagerDbContext dbContext)
        {
            _context = dbContext;
        }

        public bool Create(Invoice entity)
        {
            _context.Invoices.Add(entity);
            _context.SaveChanges();
            return true;
        }

        public IReadOnlyDictionary<int, Invoice> Get()
        {
            // Подгружаю элементы из БД
            IEnumerable<Invoice> entityColl = _context
                .Invoices
                .Where(c => c.IsDeleted == false)
                .AsEnumerable();

            Dictionary<int, Invoice> entitysIndex = entityColl
                .ToDictionary(c => c.Id, c => c);

            IReadOnlyDictionary<int, Invoice> result = entitysIndex;
            return result;
        }

        public Invoice GetById(int id)
        {
            return _context.Invoices.SingleOrDefault(c => c.Id == id);
        }

        public bool UpdateById(int id, string reqColumnName, string value)
        {
            Invoice entity = GetById(id);
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
            Invoice entity = GetById(id);
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
