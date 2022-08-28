using System.Collections.Generic;

namespace WorkManager.MySQLsettings
{
	/// <summary> Ключи имен таблиц </summary>
	public enum Tables
	{
		Clients,
		ClientContracts,
	}
	
	public class MySqlTables
    {
		/// <summary>Словарь для хранения имен таблиц</summary>
		private readonly Dictionary<Tables, string> _tablesNames = new Dictionary<Tables, string>
		{
			{Tables.Clients, "clients" },
			{Tables.ClientContracts, "clientContracts" },
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
