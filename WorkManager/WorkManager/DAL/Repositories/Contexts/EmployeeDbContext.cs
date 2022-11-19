using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using WorkManager.DAL.Models;
using WorkManager.DAL.Models.Archive;
using WorkManager.MySQLsettings;

namespace WorkManager.DAL.Repositories.Contexts
{
    internal sealed class EmployeeDbContext : MainDBContext<Employee, MyTables, EmployeesColumns>
    {
        public EmployeeDbContext(IServiceProvider provider)
        {
            _provider = provider;
            _sqlSettings = provider.GetService<IMySqlSettings<MyTables, EmployeesColumns>>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(SqlTables.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var entityTypeBuilder = modelBuilder.Entity<Employee>();

            entityTypeBuilder.Property(c => c.Id).HasColumnName(_sqlSettings[EmployeesColumns.Id]);
            entityTypeBuilder.Property(c => c.FirstName).HasColumnName(_sqlSettings[EmployeesColumns.FirstName]);
            entityTypeBuilder.Property(c => c.LastName).HasColumnName(_sqlSettings[EmployeesColumns.LastName]);
            entityTypeBuilder.Property(c => c.Email).HasColumnName(_sqlSettings[EmployeesColumns.Email]);
            entityTypeBuilder.Property(c => c.Age).HasColumnName(_sqlSettings[EmployeesColumns.Age]);
            entityTypeBuilder.Property(c => c.HourSalary).HasColumnName(_sqlSettings[EmployeesColumns.HourSalary]);
            entityTypeBuilder.Property(c => c.SpendingTime).HasColumnName(_sqlSettings[EmployeesColumns.SpendingTime]);
            entityTypeBuilder.Property(c => c.IsDeleted).HasColumnName(_sqlSettings[EmployeesColumns.IsDeleted]);
        }
    }
}
