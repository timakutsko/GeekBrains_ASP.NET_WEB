using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using WorkManager.DAL.Models.Archive;
using WorkManager.MySQLsettings;

namespace WorkManager.DAL.Repositories.Contexts
{
    internal sealed class UserDbContext : MainDBContext<User, MyTables, UsersColumns>
    {
        public UserDbContext(IServiceProvider provider)
        {
            _provider = provider;
            _sqlSettings = _provider.GetService<IMySqlSettings<MyTables, UsersColumns>>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(SqlTables.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var entityTypeBuilder = modelBuilder.Entity<User>();

            entityTypeBuilder.Property(c => c.Id).HasColumnName(_sqlSettings[UsersColumns.Id]);
            entityTypeBuilder.Property(c => c.Login).HasColumnName(_sqlSettings[UsersColumns.Login]);
            entityTypeBuilder.Property(c => c.PasswordSalt).HasColumnName(_sqlSettings[UsersColumns.PasswordSalt]);
            entityTypeBuilder.Property(c => c.PasswordHash).HasColumnName(_sqlSettings[UsersColumns.PasswordHash]);
            entityTypeBuilder.Property(c => c.RefreshToken).HasColumnName(_sqlSettings[UsersColumns.RefreshToken]);
            entityTypeBuilder.Property(c => c.IsDeleted).HasColumnName(_sqlSettings[UsersColumns.IsDeleted]);
        }
    }
}
