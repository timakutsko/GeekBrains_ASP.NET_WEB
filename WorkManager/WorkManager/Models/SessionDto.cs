using WorkManager.Tokens;

namespace WorkManager.Models
{
    public sealed class SessionDto
    {
        public int SessionId { get; set; }

        // Токен доступа
        public string SessionToken { get; set; }

        public AccountDto AccountDto { get; set; }
    }
}
