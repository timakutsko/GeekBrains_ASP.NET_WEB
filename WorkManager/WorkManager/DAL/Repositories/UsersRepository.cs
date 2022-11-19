using System.Collections.Generic;
using System.Linq;
using WorkManager.DAL.Repositories.Interfaces;
using WorkManager.Data.Contexts;
using WorkManager.Data.Models;

namespace WorkManager.DAL.Repositories
{
    internal sealed class UsersRepository : IUserRepository
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly WorkManagerDbContext _context;

        public UsersRepository(WorkManagerDbContext dbContext)
        {
            _context = dbContext;
        }

        public IReadOnlyDictionary<int, User> Get()
        {
            try
            {
                // Подгружаю элементы из БД
                IEnumerable<User> entityColl = _context
                    .Users
                    .Where(c => c.IsDeleted == false)
                    .AsEnumerable();

                Dictionary<int, User> entitysIndex = entityColl
                    .ToDictionary(c => c.Id, c => c);

                IReadOnlyDictionary<int, User> result = entitysIndex;
                return result;
            }
            catch
            {
                return null;
            }
        }

        public bool Create(User user)
        {
            // поиск свободного Id
            int tempId = 1;
            IReadOnlyDictionary<int, User> usersDict = Get();
            foreach (int keyId in usersDict.Keys)
            {
                tempId++;
            }

            user.Id = tempId;
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetRefreshToken(string login, string passwordHash, string refreshToken)
        {
            try
            {
                User currentUser = _context.Users.SingleOrDefault(c =>
                    c.Login == login
                    &&
                    c.PasswordHash == passwordHash);

                currentUser.RefreshToken = refreshToken;

                _context.Users.Update(currentUser);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
