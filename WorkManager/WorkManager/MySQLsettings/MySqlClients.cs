using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManager.MySQLsettings
{
	/// <summary> Ключи имен рядов </summary>
	public enum ClientsColumns
	{
		Id,
		FirstName,
		LastName,
		Email,
		Age,
		//ClientContract,
		Company,
		IsDeleted
	}

	/// <summary>Класс для хранения настроек базы данных</summary>
	public class MySqlClients : MySqlTables, IMySqlSettings<Tables, ClientsColumns>
	{
		/// <summary>Словарь для хранения имен рядов в таблицах</summary>
		private readonly Dictionary<ClientsColumns, string> _rowsNames = new Dictionary<ClientsColumns, string>
		{
			{ClientsColumns.Id, "Id" },
			{ClientsColumns.FirstName, "FirstName" },
			{ClientsColumns.LastName, "LastName" },
			{ClientsColumns.Email, "Email" },
			{ClientsColumns.Age, "Age" },
			//{Columns.ClientContract, "clientContract" },
			{ClientsColumns.Company, "Company" },
			{ClientsColumns.IsDeleted, "IsDeleted" },
		};

		public string this[ClientsColumns key]
		{
			get
			{
				return _rowsNames[key];
			}
		}

	}
}
