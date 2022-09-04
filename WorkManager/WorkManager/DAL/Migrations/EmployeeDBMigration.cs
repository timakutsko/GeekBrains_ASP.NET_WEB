using FluentMigrator;
using WorkManager.MySQLsettings;
using System;

namespace WorkManager.DAL.Migrations
{
	// имена таблиц
	[Migration(4)]
	public class EmployeeDBMigration : Migration
	{
		/// <summary>
		/// Объект с именами и настройками базы данных
		/// </summary>
		private readonly IMySqlSettings<Tables, EmployeesColumns> mySql;

		public EmployeeDBMigration(IMySqlSettings<Tables, EmployeesColumns> mySqlSettings)
		{
			mySql = mySqlSettings;
		}

		public override void Up()
		{
			Create.Table(mySql[Tables.Employees])
				.WithColumn(mySql[EmployeesColumns.Id]).AsInt64().PrimaryKey().Identity()
				.WithColumn(mySql[EmployeesColumns.FirstName]).AsString()
				.WithColumn(mySql[EmployeesColumns.LastName]).AsString()
				.WithColumn(mySql[EmployeesColumns.Email]).AsString()
				.WithColumn(mySql[EmployeesColumns.Age]).AsInt32()
				.WithColumn(mySql[EmployeesColumns.HourSalary]).AsInt32()
				.WithColumn(mySql[EmployeesColumns.SpendingTime]).AsTime()
				.WithColumn(mySql[EmployeesColumns.IsDeleted]).AsBoolean();
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