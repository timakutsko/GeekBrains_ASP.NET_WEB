using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using WorkManager.DAL.Interfaces;
using WorkManager.DAL.Models;
using WorkManager.MySQLsettings;

namespace WorkManager.DAL.Repositories.Contexts
{
    public class InvoiceDbContext : DbContext
    {
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private readonly IMySqlSettings<Tables, InvoicesColumns> _sqlSettings;

        public IMySqlSettings<Tables, InvoicesColumns> MySqlSettings { get { return _sqlSettings; } }
        
        /// <summary>
        /// Все элементы из БД
        /// </summary>
        public DbSet<Invoice> Invoices { get; set; }

        public InvoiceDbContext(IServiceProvider provider)
        {
            _provider = provider;
            _sqlSettings = _provider.GetService<IMySqlSettings<Tables, InvoicesColumns>>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        { 
            optionsBuilder.UseSqlite(MySqlTables.ConnectionString); 
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { 
            base.OnModelCreating(modelBuilder);
            var entityTypeBuilder = modelBuilder.Entity<Invoice>();

            entityTypeBuilder.Property(c => c.Id).HasColumnName(_sqlSettings[InvoicesColumns.Id]);
            entityTypeBuilder.Property(c => c.Title).HasColumnName(_sqlSettings[InvoicesColumns.Title]);
            entityTypeBuilder.Property(c => c.FullTime).HasColumnName(_sqlSettings[InvoicesColumns.FullTime]);
            entityTypeBuilder.Property(c => c.FullTime).HasColumnName(_sqlSettings[InvoicesColumns.Price]);
            entityTypeBuilder.Property(c => c.FullTime).HasColumnName(_sqlSettings[InvoicesColumns.CurrentContractIds]);
        }
    }
}
