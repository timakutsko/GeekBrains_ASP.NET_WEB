using System.Collections.Generic;
using System.Linq;
using WorkManager.Repositories.Interfaces;
using WorkManager.Data.Contexts;
using WorkManager.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WorkManager.Repositories
{
    internal sealed class AccountsRepository : IUserRepository
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly WorkManagerDbContext _context;

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AccountsRepository(IServiceProvider provider, IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;

            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            WorkManagerDbContext _context = scope.ServiceProvider.GetRequiredService<WorkManagerDbContext>();
        }

        public IReadOnlyDictionary<int, Account> Get()
        {
            // Подгружаю элементы из БД
            IEnumerable<Account> entityColl = _context
                .Accounts
                .Where(c => c.IsDeleted == false)
                .AsEnumerable();

            Dictionary<int, Account> entitysIndex = entityColl
                .ToDictionary(c => c.Id, c => c);

            IReadOnlyDictionary<int, Account> result = entitysIndex;
            return result;
        }

        public bool Create(Account user)
        {
            _context.Accounts.Add(user);
            _context.SaveChanges();
            return true;
        }

        public bool SetRefreshToken(string login, string passwordHash, string refreshToken)
        {
            Account currentUser = _context.Accounts.SingleOrDefault(c =>
                c.Login == login
                &&
                c.PasswordHash == passwordHash);
            if (currentUser != null)
            {
                currentUser.RefreshToken = refreshToken;

                _context.Accounts.Update(currentUser);
                _context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
