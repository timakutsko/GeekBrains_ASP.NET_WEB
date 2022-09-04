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
	public class InvoicesRepository : IRepository<int, Invoice>
	{
		/// <summary>
		/// Контекст БД
		/// </summary>
		private readonly InvoiceDbContext _context;


		public InvoicesRepository(InvoiceDbContext dbContext)
		{
			_context = dbContext;
		}

		public bool Create(Invoice entity)
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

        public IReadOnlyDictionary<int, Invoice> Get()
		{
			try
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
			catch
			{
				return null;
			}
		}

        public Invoice GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateById(int id, string reqColumnName, string value)
		{
			try
			{
				Invoice entity = _context.Invoices.SingleOrDefault(c => c.Id == id);
				foreach (InvoicesColumns column in Enum.GetValues(typeof(InvoicesColumns)))
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
			throw new NotImplementedException();
		}
	}
}
