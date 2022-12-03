using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using WorkManager.DAL.Models;
using WorkManager.MySQLsettings;

namespace WorkManager.DAL.Repositories
{
	public class ClientsRepository
    {
		/// <summary>
		/// Объект с именами и настройками базы данных
		/// </summary>
		private readonly IMySqlSettings _mySql;

		public ClientsRepository(IMySqlSettings mySqlSettings)
		{
			_mySql = mySqlSettings;
		}

		/// <summary>
		/// Записывает клиента в базу данных
		/// </summary>
		/// <param name="entity">Клиент для записи</param>
		public void Create(Client entity)
		{
			using (var connection = new SQLiteConnection(_mySql.ConnectionString))
			{
				string sqlQuery = $"INSERT INTO {_mySql[Tables.Clients]}" +
				$"({_mySql[Columns.Id]}, {_mySql[Columns.FirstName]}, {_mySql[Columns.LastName]}, {_mySql[Columns.Email]}, " +
				$"{_mySql[Columns.Age]}, {_mySql[Columns.Company]})" +
				$"VALUES (@id, @firstName, @lastName, @email, @age, @company);";

				connection.ExecuteAsync(sqlQuery,
				new
				{
					id = entity.Id,
					firstName = entity.FirstName,
					lastName = entity.LastName,
					email = entity.Email,
					age = entity.Age,
					company = entity.Company,
				});
			}
		}

		/// <summary>
		/// Возвращает список всех клиентов
		/// </summary>
		/// <returns>Список всех клиентов</returns>
		//public IList<Client> GetByEntities()
		//{
		//	var returnList = new List<Client>();
		//	using (var connection = new SQLiteConnection(_mySql.ConnectionString))
		//	{
		//		return connection.Query<Client>(
		//		"SELECT * " +
		//		$"FROM {_mySql[Tables.Clients]} " +
		//		$"WHERE ({_mySql[Columns.Time]} >= @fromTime AND {_mySql[Columns.Time]} <= @toTime)",
		//		new
		//		{
		//			fromTime = fromTime.ToUnixTimeSeconds(),
		//			toTime = toTime.ToUnixTimeSeconds(),
		//		}).ToList();
		//	}
		//}
	}
}
