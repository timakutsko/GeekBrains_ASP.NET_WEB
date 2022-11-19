using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WorkManager.DAL.Repositories.Interfaces;
using WorkManager.Data.Contexts;
using WorkManager.Data.Models;

namespace WorkManager.DAL.Repositories
{
	internal sealed class ClientContractsRepository : IRepository<int, ClientContract>
	{
		/// <summary>
		/// Контекст БД
		/// </summary>
		private readonly WorkManagerDbContext _context;

		public ClientContractsRepository(WorkManagerDbContext dbContext)
		{
			_context = dbContext;
		}

		public bool Create(ClientContract entity)
		{
			_context.ClientContracts.Add(entity);
			_context.SaveChanges();
			return true;
		}

		public IReadOnlyDictionary<int, ClientContract> Get()
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

		public ClientContract GetById(int id)
		{
			return _context.ClientContracts.SingleOrDefault(c => c.Id == id);
		}

		public bool UpdateById(int id, string reqColumnName, string value)
		{
			ClientContract entity = GetById(id);
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
			ClientContract entity = GetById(id);
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
