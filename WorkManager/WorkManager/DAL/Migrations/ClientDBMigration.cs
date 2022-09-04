using FluentMigrator;
using WorkManager.MySQLsettings;
using System;

namespace WorkManager.DAL.Migrations
{
	// имена таблиц
	[Migration(1)]
	public class ClientDBMigration : Migration
	{
		/// <summary>
		/// Объект с именами и настройками базы данных
		/// </summary>
		private readonly IMySqlSettings<Tables, ClientsColumns> mySql;

		public ClientDBMigration(IMySqlSettings<Tables, ClientsColumns> mySqlSettings)
		{
			mySql = mySqlSettings;
		}

		public override void Up()
		{
			Create.Table(mySql[Tables.Clients])
				.WithColumn(mySql[ClientsColumns.Id]).AsInt64().PrimaryKey().Identity()
				.WithColumn(mySql[ClientsColumns.FirstName]).AsString()
				.WithColumn(mySql[ClientsColumns.LastName]).AsString()
				.WithColumn(mySql[ClientsColumns.Email]).AsString()
				.WithColumn(mySql[ClientsColumns.Age]).AsInt32()
				.WithColumn(mySql[ClientsColumns.Company]).AsString()
				.WithColumn(mySql[ClientsColumns.IsDeleted]).AsBoolean();
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