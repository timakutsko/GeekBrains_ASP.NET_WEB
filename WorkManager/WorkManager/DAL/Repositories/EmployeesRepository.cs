using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WorkManager.DAL.Models;
using WorkManager.DAL.Repositories.Contexts;
using WorkManager.MySQLsettings;
using WorkManager.Repositories.Interfaces;

namespace WorkManager.DAL.Repositories
{
    public class EmployeesRepository : IRepository<int, Employee>
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly EmployeeDbContext _context;


        public EmployeesRepository(EmployeeDbContext dbContext)
        {
            _context = dbContext;
        }

        public bool Create(Employee entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IReadOnlyDictionary<int, Employee> Get()
        {
            try
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
            catch
            {
                return null;
            }
        }

        public Employee GetById(int id)
        {
            try
            {
                return _context.Employees.SingleOrDefault(c => c.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateById(int id, string reqColumnName, string value)
        {
            try
            {
                Employee entity = _context.Employees.SingleOrDefault(c => c.Id == id);
                foreach (EmployeesColumns column in Enum.GetValues(typeof(EmployeesColumns)))
                {
                    string dbColumnName = _context.MySqlSettings[column];
                    if (dbColumnName == reqColumnName)
                    {
                        PropertyInfo prop = entity.GetType().GetProperty(dbColumnName, BindingFlags.Public | BindingFlags.Instance);
                        if (null != prop && prop.CanWrite)
                        {
                            prop.SetValue(entity, value, null);
                            _context.Update(entity);
                            _context.SaveChanges();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            try
            {
                Employee entity = _context.Employees.SingleOrDefault(c => c.Id == id);
                _context.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
