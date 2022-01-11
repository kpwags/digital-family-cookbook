namespace DigitalFamilyCookbook.Data.Models;

public class AuthResult
{
    public string Token { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public bool IsSuccessful { get; set; }

    public IReadOnlyCollection<string> Errors { get; set; } = Array.Empty<string>();
}