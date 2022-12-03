namespace WorkManager.Tokens
{
    public sealed class ContainerTokens
    {
        // Токен доступа
        public string AccessToken { get; set; }

        // Токен обновления
        public RefreshToken RefreshToken { get; set; }
    }
}
