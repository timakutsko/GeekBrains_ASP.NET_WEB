using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManager.MySQLsettings
{
	/// <summary> Ключи имен рядов </summary>
	public enum EmployeesColumns
	{
		Id,
		FirstName,
		LastName,
		Email,
		Age,
		HourSalary,
		SpendingTime,
		IsDeleted
	}

	/// <summary>Класс для хранения настроек базы данных</summary>
	public class MySqlEmployees : MySqlTables, IMySqlSettings<Tables, EmployeesColumns>
	{
		/// <summary>Словарь для хранения имен рядов в таблицах</summary>
		private readonly Dictionary<EmployeesColumns, string> _rowsNames = new Dictionary<EmployeesColumns, string>
		{
			{EmployeesColumns.Id, "Id" },
			{EmployeesColumns.FirstName, "FirstName" },
			{EmployeesColumns.LastName, "LastName" },
			{EmployeesColumns.Email, "Email" },
			{EmployeesColumns.Age, "Age" },
			{EmployeesColumns.HourSalary, "HourSalary" },
			{EmployeesColumns.SpendingTime, "SpendingTime" },
			{EmployeesColumns.IsDeleted, "IsDeleted" },
		};

		public string this[EmployeesColumns key]
		{
			get
			{
				return _rowsNames[key];
			}
		}
	}
}
