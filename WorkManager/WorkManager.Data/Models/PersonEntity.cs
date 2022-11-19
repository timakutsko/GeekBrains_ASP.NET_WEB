using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkManager.Data.Models
{
    public class PersonEntity
    {
        /// <summary>
        /// Уникальный идентификатор персоны
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Имя персоны
        /// </summary>
        [Column(TypeName = "nvarchar(128)")]
        public string? FirstName { get; set; }

        /// <summary>
        /// Фамилия персоны
        /// </summary>
        [Column(TypeName = "nvarchar(128)")]
        public string? LastName { get; set; }

        /// <summary>
        /// E-mail персоны
        /// </summary>
        [Column(TypeName = "nvarchar(256)")]
        public string? Email { get; set; }

        /// <summary>
        /// Возраст персоны
        /// </summary>
        [Column(TypeName = "int")]
        public int Age { get; set; }

        /// <summary>
        /// Проверка на удаление персоны (архивация)
        /// </summary>
        [Column(TypeName = "int")]
        public bool? IsDeleted { get; set; }
    }
}
