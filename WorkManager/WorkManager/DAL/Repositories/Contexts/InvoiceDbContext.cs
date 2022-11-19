using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using WorkManager.DAL.Models.Archive;
using WorkManager.MySQLsettings;

namespace WorkManager.DAL.Repositories.Contexts
{
    internal sealed class InvoiceDbContext : MainDBContext<Invoice, MyTables, InvoicesColumns>
    {
        public InvoiceDbContext(IServiceProvider provider)
        {
            _provider = provider;
            _sqlSettings = provider.GetService<IMySqlSettings<MyTables, InvoicesColumns>>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(SqlTables.ConnectionString);
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
