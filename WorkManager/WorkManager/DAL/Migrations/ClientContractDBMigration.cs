using FluentMigrator;
using WorkManager.MySQLsettings;
using System;

namespace WorkManager.DAL.Migrations
{
	// имена таблиц
	[Migration(2)]
	public class ClientContractDBMigration : Migration
	{
		/// <summary>
		/// Объект с именами и настройками базы данных
		/// </summary>
		private readonly IMySqlSettings<Tables, ClientContractsColumns> mySql;

		public ClientContractDBMigration(IMySqlSettings<Tables, ClientContractsColumns> mySqlSettings)
		{
			mySql = mySqlSettings;
		}

		public override void Up()
		{
			Create.Table(mySql[Tables.ClientContracts])
				.WithColumn(mySql[ClientContractsColumns.Id]).AsInt64().PrimaryKey().Identity()
				.WithColumn(mySql[ClientContractsColumns.Title]).AsString()
				.WithColumn(mySql[ClientContractsColumns.FullTime]).AsTime()
				.WithColumn(mySql[ClientContractsColumns.IsDeleted]).AsBoolean();
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