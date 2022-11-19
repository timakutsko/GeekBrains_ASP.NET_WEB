using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using WorkManager.DAL.Models.Archive;
using WorkManager.MySQLsettings;

namespace WorkManager.DAL.Repositories.Contexts
{
    internal sealed class ClientContractDbContext : MainDBContext<ClientContract, MyTables, ClientContractsColumns>
    {
        public ClientContractDbContext(IServiceProvider provider)
        {
            _provider = provider;
            _sqlSettings = provider.GetService<IMySqlSettings<MyTables, ClientContractsColumns>>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(SqlTables.ConnectionString);
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
