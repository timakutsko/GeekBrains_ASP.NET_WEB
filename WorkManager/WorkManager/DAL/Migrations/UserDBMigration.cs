using FluentMigrator;
using System;
using WorkManager.MySQLsettings;

namespace WorkManager.DAL.Migrations.Archive
{
	// имена таблиц
	[Migration(5)]
	public class UserDBMigration : Migration
	{
		/// <summary>
		/// Объект с именами и настройками базы данных
		/// </summary>
		private readonly IMySqlSettings<MyTables, UsersColumns> mySql;

		public UserDBMigration(IMySqlSettings<MyTables, UsersColumns> mySqlSettings)
		{
			mySql = mySqlSettings;
		}

		public override void Up()
		{
			Create.Table(mySql[MyTables.Users])
				.WithColumn(mySql[UsersColumns.Id]).AsInt64().PrimaryKey().Identity()
				.WithColumn(mySql[UsersColumns.Login]).AsString()
				.WithColumn(mySql[UsersColumns.PasswordSalt]).AsString()
				.WithColumn(mySql[UsersColumns.PasswordHash]).AsString()
				.WithColumn(mySql[UsersColumns.RefreshToken]).AsString().Nullable()
				.WithColumn(mySql[UsersColumns.IsDeleted]).AsBoolean();
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