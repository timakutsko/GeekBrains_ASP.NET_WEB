using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WorkManager.DAL.Repositories.Interfaces;
using WorkManager.Data.Contexts;
using WorkManager.Data.Models;

namespace WorkManager.DAL.Repositories
{
    internal sealed class EmployeesRepository : IRepository<int, Employee>
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly WorkManagerDbContext _context;


        public EmployeesRepository(WorkManagerDbContext dbContext)
        {
            _context = dbContext;
        }

        public bool Create(Employee entity)
        {
            _context.Employees.Add(entity);
            _context.SaveChanges();
            return true;
        }

        public IReadOnlyDictionary<int, Employee> Get()
        {
            // Подгружаю элементы из БД
            IEnumerable<Employee> entityColl = _context
                .Employees
                .Where(c => c.IsDeleted == false)
                .AsEnumerable();

            Dictionary<int, Employee> entitysIndex = entityColl
                .ToDictionary(c => c.Id, c => c);

            IReadOnlyDictionary<int, Employee> result = entitysIndex;
            return result;
        }

        public Employee GetById(int id)
        {
            return _context.Employees.SingleOrDefault(c => c.Id == id);
        }

        public bool UpdateById(int id, string reqColumnName, string value)
        {
            Employee entity = GetById(id);
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
            Employee entity = GetById(id);
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
