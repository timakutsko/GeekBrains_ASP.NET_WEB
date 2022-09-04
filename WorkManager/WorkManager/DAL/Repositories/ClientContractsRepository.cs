using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using WorkManager.DAL.Models;
using WorkManager.DAL.Repositories.Contexts;
using WorkManager.MySQLsettings;
using WorkManager.Repositories.Interfaces;

namespace WorkManager.DAL.Repositories
{
	public class ClientContractsRepository : IRepository<int, ClientContract>
	{
		/// <summary>
		/// Контекст БД
		/// </summary>
		private readonly ClientContractDbContext _context;


		public ClientContractsRepository(ClientContractDbContext dbContext)
		{
			_context = dbContext;
		}

		public bool Create(ClientContract entity)
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

		public IReadOnlyDictionary<int, ClientContract> Get()
		{
			try
			{
				// Подгружаю элементы из БД
				IEnumerable<ClientContract> entityColl = _context
					.ClientContracts
					.Where(c => c.IsDeleted == false)
					.AsEnumerable();

				Dictionary<int, ClientContract> entitysIndex = entityColl
					.ToDictionary(c => c.Id, c => c);

				IReadOnlyDictionary<int, ClientContract> result = entitysIndex;
				return result;
			}
			catch
			{
				return null;
			}
		}

		public ClientContract GetById(int id)
		{
			try
			{
				return _context.ClientContracts.SingleOrDefault(c => c.Id == id);
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
				ClientContract entity = _context.ClientContracts.SingleOrDefault(c => c.Id == id);
				foreach (ClientContractsColumns column in Enum.GetValues(typeof(ClientContractsColumns)))
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
				ClientContract entity = _context.ClientContracts.SingleOrDefault(c => c.Id == id);
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
