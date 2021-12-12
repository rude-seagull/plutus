namespace Plutus.Infrastructure.Options;

public class TokenSecurityOptions
{
    public const string TokenSecurity = "TokenSecurity";
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int DurationInMinutes { get; set; }
}