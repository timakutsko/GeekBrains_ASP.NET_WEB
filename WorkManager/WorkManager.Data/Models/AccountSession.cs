using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkManager.Data.Models
{
    [Table("AccountSessions")]
    public class AccountSession
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(384)")]
        public string? SessionToken { get; set; }

        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime TimeCreated { get; set; }

        public bool IsClosed { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? TimeClosed { get; set; }

        public virtual Account? Account { get; set; }
    }
}
