using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkManager.Data.Models
{
    [Table("Users")]
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
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string? Login { get; private set; }

        [Column(TypeName = "nvarchar(256)")]
        public string? PasswordSalt { get; private set; }

        [Column(TypeName = "nvarchar(256)")]
        public string? PasswordHash { get; private set; }

        [Column(TypeName = "nvarchar(256)")]
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Проверка на удаление пользователя (архивация)
        /// </summary>
        [Column(TypeName = "int")]
        public bool IsDeleted { get; set; }
    }
}
