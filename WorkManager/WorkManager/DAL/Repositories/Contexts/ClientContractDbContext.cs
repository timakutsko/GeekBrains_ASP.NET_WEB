using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using WorkManager.DAL.Interfaces;
using WorkManager.DAL.Models;
using WorkManager.MySQLsettings;

namespace WorkManager.DAL.Repositories.Contexts
{
    public class ClientContractDbContext : DbContext
    {
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private readonly IMySqlSettings<Tables, ClientContractsColumns> _sqlSettings;

        public IMySqlSettings<Tables, ClientContractsColumns> MySqlSettings { get { return _sqlSettings; } }
        
        /// <summary>
        /// Все элементы из БД
        /// </summary>
        public DbSet<ClientContract> ClientContracts { get; set; }

        public ClientContractDbContext(IServiceProvider provider)
        {
            _provider = provider;
            _sqlSettings = _provider.GetService<IMySqlSettings<Tables, ClientContractsColumns>>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        { 
            optionsBuilder.UseSqlite(MySqlTables.ConnectionString); 
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { 
            base.OnModelCreating(modelBuilder);
            var entityTypeBuilder = modelBuilder.Entity<ClientContract>();

            entityTypeBuilder.Property(c => c.Id).HasColumnName(_sqlSettings[ClientContractsColumns.Id]);
            entityTypeBuilder.Property(c => c.Title).HasColumnName(_sqlSettings[ClientContractsColumns.Title]);
            entityTypeBuilder.Property(c => c.FullTime).HasColumnName(_sqlSettings[ClientContractsColumns.FullTime]); 
        }
    }
}
