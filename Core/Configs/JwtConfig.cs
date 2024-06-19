namespace SocialMediaApi.Core.Config
{
    public sealed record JwtConfig
    {
        public string Issuer { get; init; }
        public int ExpirationHour { get; init; }
        public string SecretKey { get; init; }
        public IEnumerable<string> Algorithm { get; init; }
        public IEnumerable<string> Audiences { get; init; }
    }
}
