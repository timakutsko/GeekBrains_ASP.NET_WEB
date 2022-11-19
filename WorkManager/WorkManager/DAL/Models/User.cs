using System.ComponentModel.DataAnnotations.Schema;

namespace WorkManager.DAL.Models.Archive
{
    [Table("Users", Schema = "UserManager")]
    public sealed class User
    {
        public User(string login, string passwordSalt, string passwordHash, bool isDeleted=false)
        {
            Login = login;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            IsDeleted = isDeleted;
        }
        
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public int Id { get; set; }
        
        public string Login { get; private set; }
        
        public string PasswordSalt { get; private set; }

        public string PasswordHash { get; private set; }

        public string RefreshToken { get; set; }

        /// <summary>
        /// Проверка на удаление пользователя (архивация)
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
