using FluentMigrator;
using System;
using WorkManager.MySQLsettings;

namespace WorkManager.DAL.Migrations.Archive
{
	// имена таблиц
	[Migration(1)]
	public class ClientDBMigration : Migration
	{
		/// <summary>
		/// Объект с именами и настройками базы данных
		/// </summary>
		private readonly IMySqlSettings<MyTables, ClientsColumns> mySql;

		public ClientDBMigration(IMySqlSettings<MyTables, ClientsColumns> mySqlSettings)
		{
			mySql = mySqlSettings;
		}

		public override void Up()
		{
			Create.Table(mySql[MyTables.Clients])
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
			foreach (MyTables tableName in Enum.GetValues(typeof(MyTables)))
			{
				Delete.Table(mySql[tableName]);
			}

		}
	}
}