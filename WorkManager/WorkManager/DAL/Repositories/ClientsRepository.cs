﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WorkManager.DAL.Models;
using WorkManager.DAL.Repositories.Contexts;
using WorkManager.MySQLsettings;
using WorkManager.Repositories.Interfaces;

namespace WorkManager.DAL.Repositories
{
    public class ClientsRepository : IRepository<int, Client>
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly ClientDbContext _context;


        public ClientsRepository(ClientDbContext dbContext)
        {
            _context = dbContext;
        }

        public bool Create(Client entity)
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

        public IReadOnlyDictionary<int, Client> Get()
        {
            try
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
            catch
            {
                return null;
            }
        }

        public Client GetById(int id)
        {
            try
            {
                return _context.Clients.SingleOrDefault(c => c.Id == id);
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
                Client entity = _context.Clients.SingleOrDefault(c => c.Id == id);
                foreach (ClientsColumns column in Enum.GetValues(typeof(ClientsColumns)))
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
                Client entity = _context.Clients.SingleOrDefault(c => c.Id == id);
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
