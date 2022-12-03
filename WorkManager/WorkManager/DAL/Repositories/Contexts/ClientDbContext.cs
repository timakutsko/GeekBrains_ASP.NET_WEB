using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using WorkManager.DAL.Interfaces;
using WorkManager.DAL.Models;
using WorkManager.MySQLsettings;

namespace WorkManager.DAL.Repositories.Contexts
{
    public class ClientDbContext : DbContext
    {
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private readonly IMySqlSettings<Tables, ClientsColumns> _sqlSettings;

        public IMySqlSettings<Tables, ClientsColumns> MySqlSettings { get { return _sqlSettings; } }
        
        /// <summary>
        /// Все элементы из БД
        /// </summary>
        public DbSet<Client> Clients { get; set; }

        public ClientDbContext(IServiceProvider provider)
        {
            _provider = provider;
            _sqlSettings = _provider.GetService<IMySqlSettings<Tables, ClientsColumns>>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        { 
            optionsBuilder.UseSqlite(MySqlTables.ConnectionString); 
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { 
            base.OnModelCreating(modelBuilder);
            var entityTypeBuilder = modelBuilder.Entity<Client>();

            entityTypeBuilder.Property(c => c.Id).HasColumnName(_sqlSettings[ClientsColumns.Id]); 
            entityTypeBuilder.Property(c => c.FirstName).HasColumnName(_sqlSettings[ClientsColumns.FirstName]); 
            entityTypeBuilder.Property(c => c.LastName).HasColumnName(_sqlSettings[ClientsColumns.LastName]); 
            entityTypeBuilder.Property(c => c.Email).HasColumnName(_sqlSettings[ClientsColumns.Email]); 
            entityTypeBuilder.Property(c => c.Age).HasColumnName(_sqlSettings[ClientsColumns.Age]); 
            entityTypeBuilder.Property(c => c.Company).HasColumnName(_sqlSettings[ClientsColumns.Company]);
            entityTypeBuilder.Property(c => c.IsDeleted).HasColumnName(_sqlSettings[ClientsColumns.IsDeleted]); 
        }
    }
}
