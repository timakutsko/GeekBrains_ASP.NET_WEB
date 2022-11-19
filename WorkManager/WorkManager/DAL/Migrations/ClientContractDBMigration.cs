using FluentMigrator;
using System;
using WorkManager.MySQLsettings;

namespace WorkManager.DAL.Migrations.Archive
{
	// имена таблиц
	[Migration(2)]
	public class ClientContractDBMigration : Migration
	{
		/// <summary>
		/// Объект с именами и настройками базы данных
		/// </summary>
		private readonly IMySqlSettings<MyTables, ClientContractsColumns> mySql;

		public ClientContractDBMigration(IMySqlSettings<MyTables, ClientContractsColumns> mySqlSettings)
		{
			mySql = mySqlSettings;
		}

		public override void Up()
		{
			Create.Table(mySql[MyTables.ClientContracts])
				.WithColumn(mySql[ClientContractsColumns.Id]).AsInt64().PrimaryKey().Identity()
				.WithColumn(mySql[ClientContractsColumns.Title]).AsString()
				.WithColumn(mySql[ClientContractsColumns.FullTime]).AsTime()
				.WithColumn(mySql[ClientContractsColumns.IsDeleted]).AsBoolean();
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