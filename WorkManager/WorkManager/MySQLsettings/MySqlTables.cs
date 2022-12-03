using System.Collections.Generic;

namespace WorkManager.MySQLsettings
{
	/// <summary> Ключи имен таблиц </summary>
	public enum Tables
	{
		Clients,
		ClientContracts,
		Invoices,
		Employees
	}
	
	public class MySqlTables
    {
		/// <summary> Строка для подключения к базе данных </summary>
		private static readonly string _connectionString = @"Data Source=WorkManager.db";

		public static string ConnectionString
		{
			get
			{
				return _connectionString;
			}
		}

		/// <summary>Словарь для хранения имен таблиц</summary>
		private readonly Dictionary<Tables, string> _tablesNames = new Dictionary<Tables, string>
		{
			{Tables.Clients, "Clients" },
			{Tables.ClientContracts, "ClientContracts" },
			{Tables.Invoices, "Invoices" },
			{Tables.Employees, "Employees" },
		};

		public string this[Tables key]
		{
			get
			{
				return _tablesNames[key];
			}
		}
	}
}
