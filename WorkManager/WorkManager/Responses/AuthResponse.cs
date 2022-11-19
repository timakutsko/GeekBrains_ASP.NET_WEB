using WorkManager.Data.Models;
using WorkManager.Tokens;

namespace WorkManager.Responses
{
    internal static class AuthResponse
    {
        public static User CurrentUser { get; set; }

        public static RefreshToken LatestRefreshToken { get; set; }
    }
}
