using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManager.MySQLsettings
{
	/// <summary> Ключи имен рядов </summary>
	public enum InvoicesColumns
	{
		Id,
		Title,
		FullTime,
		IsDeleted,
		Price,
		CurrentContractIds
}

	/// <summary>Класс для хранения настроек базы данных</summary>
	public class MySqlInvoices : MySqlTables, IMySqlSettings<Tables, InvoicesColumns>
	{
		/// <summary>Словарь для хранения имен рядов в таблицах</summary>
		private readonly Dictionary<InvoicesColumns, string> _rowsNames = new Dictionary<InvoicesColumns, string>
		{
			{InvoicesColumns.Id, "Id" },
			{InvoicesColumns.Title, "Title" },
			{InvoicesColumns.FullTime, "FullTime" },
			{InvoicesColumns.Price, "Price" },
			{InvoicesColumns.CurrentContractIds, "CurrentContractIds" },
			{InvoicesColumns.IsDeleted, "IsDeleted" },
		};

		public string this[InvoicesColumns key]
		{
			get
			{
				return _rowsNames[key];
			}
		}

	}
}
