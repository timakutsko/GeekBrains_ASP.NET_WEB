using WorkManager.Data.Models;
using WorkManager.Models;
using WorkManager.Tokens;

namespace WorkManager.Responses
{
    internal class AuthenticateDto
    {
        public AccountDto AccountDto { get; set; }
        
        public SessionDto SessionDto { get; set; }

        public AuthenticationStatus Status { get; set; }

        public RefreshToken LatestRefreshToken { get; set; }
    }
}
