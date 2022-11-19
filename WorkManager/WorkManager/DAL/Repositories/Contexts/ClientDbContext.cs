using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using WorkManager.DAL.Models.Archive;
using WorkManager.MySQLsettings;

namespace WorkManager.DAL.Repositories.Contexts
{
    internal sealed class ClientDbContext : MainDBContext<Client, MyTables, ClientsColumns>
    {
        public ClientDbContext(IServiceProvider provider)
        {
            _provider = provider;
            _sqlSettings = provider.GetService<IMySqlSettings<MyTables, ClientsColumns>>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(SqlTables.ConnectionString);
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
