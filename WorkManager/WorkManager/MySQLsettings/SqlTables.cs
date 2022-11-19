using System.Collections.Generic;

namespace WorkManager.MySQLsettings
{
	/// <summary> Ключи имен таблиц для БД WorkManager</summary>
	public enum MyTables
	{
		Clients,
		ClientContracts,
		Invoices,
		Employees,
		Users
	}

	internal class SqlTables
	{
		private static readonly string _connectionString = @"Data Source=WorkManager.db";

		/// <summary> Строка для подключения к базе данных </summary>
		public static string ConnectionString
		{
			get
			{
				return _connectionString;
			}
		}

		/// <summary>Словарь для хранения имен таблиц</summary>
		private readonly Dictionary<MyTables, string> _tablesNames = new Dictionary<MyTables, string>
		{
			{MyTables.Clients, "Clients" },
			{MyTables.ClientContracts, "ClientContracts" },
			{MyTables.Invoices, "Invoices" },
			{MyTables.Employees, "Employees" },
			{MyTables.Users, "Users" },
		};

		public string this[MyTables key]
		{
			get
			{
				return _tablesNames[key];
			}
		}
	}
}
