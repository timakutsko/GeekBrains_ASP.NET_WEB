using Microsoft.EntityFrameworkCore;
using WorkManager.Data.Models;

namespace WorkManager.Data.Contexts
{
    public sealed class WorkManagerDbContext : DbContext
    {
        public DbSet<Client>? Clients { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<ClientContract>? ClientContracts { get; set; }
        public DbSet<Invoice>? Invoices { get; set; }
        public DbSet<User>? Users { get; set; }
        
        public WorkManagerDbContext(DbContextOptions<WorkManagerDbContext> options) : base(options)
        {
        }

    }
}