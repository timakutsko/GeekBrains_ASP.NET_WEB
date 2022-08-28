using FluentMigrator;
using WorkManager.MySQLsettings;
using System;

namespace WorkManager.DAL.Migrations
{
	// имена таблиц
	[Migration(1)]
	public class MyMigration : Migration
	{
		/// <summary>
		/// Объект с именами и настройками базы данных
		/// </summary>
		private readonly IMySqlSettings mySql;

		public MyMigration(IMySqlSettings mySqlSettings)
		{
			mySql = mySqlSettings;
		}

		public override void Up()
		{
			foreach(Tables tableName in Enum.GetValues(typeof(Tables)))
			{
				Create.Table(mySql[tableName])
					.WithColumn(mySql[Columns.Id]).AsInt64().PrimaryKey().Identity()
					.WithColumn(mySql[Columns.FirstName]).AsString()
					.WithColumn(mySql[Columns.LastName]).AsString()
					.WithColumn(mySql[Columns.Email]).AsString()
					.WithColumn(mySql[Columns.Age]).AsInt32()
					.WithColumn(mySql[Columns.Company]).AsString();
			}
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