using FluentMigrator;
using WorkManager.MySQLsettings;
using System;

namespace WorkManager.DAL.Migrations
{
	// имена таблиц
	[Migration(3)]
	public class InvoiceDBMigration : Migration
	{
		/// <summary>
		/// Объект с именами и настройками базы данных
		/// </summary>
		private readonly IMySqlSettings<Tables, InvoicesColumns> mySql;

		public InvoiceDBMigration(IMySqlSettings<Tables, InvoicesColumns> mySqlSettings)
		{
			mySql = mySqlSettings;
		}

		public override void Up()
		{
			Create.Table(mySql[Tables.Invoices])
				.WithColumn(mySql[InvoicesColumns.Id]).AsInt64().PrimaryKey().Identity()
				.WithColumn(mySql[InvoicesColumns.Title]).AsString()
				.WithColumn(mySql[InvoicesColumns.FullTime]).AsTime()
				.WithColumn(mySql[InvoicesColumns.Price]).AsInt64()
				.WithColumn(mySql[InvoicesColumns.CurrentContractIds]).AsString()
				.WithColumn(mySql[InvoicesColumns.IsDeleted]).AsBoolean();
		}

		public override void Down()
		{
			foreach (Tables tableName in Enum.GetValues(typeof(Tables)))
			{
				Delete.Table(mySql[tableName]);
			}

		}
	}
}