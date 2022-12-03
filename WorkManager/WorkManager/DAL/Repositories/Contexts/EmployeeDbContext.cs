using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using WorkManager.DAL.Interfaces;
using WorkManager.DAL.Models;
using WorkManager.MySQLsettings;

namespace WorkManager.DAL.Repositories.Contexts
{
    public class EmployeeDbContext : DbContext
    {
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private readonly IMySqlSettings<Tables, EmployeesColumns> _sqlSettings;

        public IMySqlSettings<Tables, EmployeesColumns> MySqlSettings { get { return _sqlSettings; } }
        
        /// <summary>
        /// Все элементы из БД
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        public EmployeeDbContext(IServiceProvider provider)
        {
            _provider = provider;
            _sqlSettings = _provider.GetService<IMySqlSettings<Tables, EmployeesColumns>>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        { 
            optionsBuilder.UseSqlite(MySqlTables.ConnectionString); 
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
