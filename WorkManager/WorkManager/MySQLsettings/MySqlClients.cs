using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManager.MySQLsettings
{
	/// <summary> Ключи имен рядов </summary>
	public enum Columns
	{
		Id,
		FirstName,
		LastName,
		Email,
		Age,
		//ClientContract,
		Company,
	}

	/// <summary>Класс для хранения настроек базы данных</summary>
	public class MySqlClients : MySqlTables, IMySqlSettings
	{
		/// <summary>Словарь для хранения имен рядов в таблицах</summary>
		private readonly Dictionary<Columns, string> _rowsNames = new Dictionary<Columns, string>
		{
			{Columns.Id, "id" },
			{Columns.FirstName, "firstName" },
			{Columns.LastName, "lastName" },
			{Columns.Email, "email" },
			{Columns.Age, "age" },
			//{Columns.ClientContract, "clientContract" },
			{Columns.Company, "company" },
		};

		/// <summary> Строка для подключения к базе данных </summary>
		private readonly string _connectionString = @"Data Source=WorkManager.db; Version=3;Pooling=True;Max Pool Size=100;";

		public string ConnectionString
		{
			get
			{
				return _connectionString;
			}
		}

		public string this[Columns key]
		{
			get
			{
				return _rowsNames[key];
			}
		}

	}
}
