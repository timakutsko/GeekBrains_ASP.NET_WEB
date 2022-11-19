using FluentMigrator;
using System;
using WorkManager.MySQLsettings;

namespace WorkManager.DAL.Migrations.Archive
{
	// имена таблиц
	[Migration(4)]
	public class EmployeeDBMigration : Migration
	{
		/// <summary>
		/// Объект с именами и настройками базы данных
		/// </summary>
		private readonly IMySqlSettings<MyTables, EmployeesColumns> mySql;

		public EmployeeDBMigration(IMySqlSettings<MyTables, EmployeesColumns> mySqlSettings)
		{
			mySql = mySqlSettings;
		}

		public override void Up()
		{
			Create.Table(mySql[MyTables.Employees])
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
			foreach (MyTables tableName in Enum.GetValues(typeof(MyTables)))
			{
				Delete.Table(mySql[tableName]);
			}

		}
	}
}