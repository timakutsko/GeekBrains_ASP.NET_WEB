using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkManager.Data.Models
{
    public class WorkResultEntity
    {
        /// <summary>
        /// Уникальный идентификатор работы
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Имя работы
        /// </summary>
        [Column(TypeName = "nvarchar(128)")]
        public string? Title { get; set; }

        /// <summary>
        /// Общее время в часах на выполнение работы
        /// </summary>
        [NotMapped]
        public TimeSpan FullTime
        {
            get { return TimeSpan.FromTicks(TimeSpanTicks); }
            set { TimeSpanTicks = value.Ticks; }
        }
        
        [Column(TypeName = "bigint")]
        public long TimeSpanTicks { get; set; }

        /// <summary>
        /// Проверка на удаление работы (архивация)
        /// </summary>
        [Required]
        [Column(TypeName = "int")]
        public bool? IsDeleted { get; set; }
    }
}
