namespace DigitalFamilyCookbook.Models;

public class AuthToken
{
    public string Token { get; set; } = string.Empty;

    public long TokenExpirationTime { get; set; }

    public string Id { get; set; } = string.Empty;
}