using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WorkManager.Data.Models;

namespace WorkManager.Data.Contexts
{
    public interface IDbContext
    {
        public DbSet<Client>? Clients { get; set; }

        public DbSet<Employee>? Employees { get; set; }

        public DbSet<ClientContract>? ClientContracts { get; set; }

        public DbSet<Invoice>? Invoices { get; set; }

        public DbSet<Account>? Accounts { get; set; }

        public DbSet<AccountSession>? AccountSessions { get; set; }

        public int SaveChanges();

        public EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
    }
    
    public class WorkManagerDbContext : DbContext, IDbContext
    {
        public DbSet<Client>? Clients { get; set; }
        
        public DbSet<Employee>? Employees { get; set; }
        
        public DbSet<ClientContract>? ClientContracts { get; set; }
        
        public DbSet<Invoice>? Invoices { get; set; }
        
        public DbSet<Account>? Accounts { get; set; }
        
        public DbSet<AccountSession>? AccountSessions { get; set; }
        
        public WorkManagerDbContext(DbContextOptions<WorkManagerDbContext> options) : base(options)
        {
        }

    }
}