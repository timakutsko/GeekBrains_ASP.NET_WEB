using FluentMigrator;
using System;
using WorkManager.MySQLsettings;

namespace WorkManager.DAL.Migrations.Archive
{
	// имена таблиц
	[Migration(3)]
	public class InvoiceDBMigration : Migration
	{
		/// <summary>
		/// Объект с именами и настройками базы данных
		/// </summary>
		private readonly IMySqlSettings<MyTables, InvoicesColumns> mySql;

		public InvoiceDBMigration(IMySqlSettings<MyTables, InvoicesColumns> mySqlSettings)
		{
			mySql = mySqlSettings;
		}

		public override void Up()
		{
			Create.Table(mySql[MyTables.Invoices])
				.WithColumn(mySql[InvoicesColumns.Id]).AsInt64().PrimaryKey().Identity()
				.WithColumn(mySql[InvoicesColumns.Title]).AsString()
				.WithColumn(mySql[InvoicesColumns.FullTime]).AsTime()
				.WithColumn(mySql[InvoicesColumns.Price]).AsInt64()
				.WithColumn(mySql[InvoicesColumns.CurrentContractIds]).AsString()
				.WithColumn(mySql[InvoicesColumns.IsDeleted]).AsBoolean();
		}

		public override void Down()
		{
			foreach (MyTables tableName in Enum.GetValues(typeof(MyTables)))
			{
				Delete.Table(mySql[tableName]);
			}

		}
	}
}