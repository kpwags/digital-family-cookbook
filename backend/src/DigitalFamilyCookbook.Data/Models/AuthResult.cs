using System.Text.Json.Serialization;

namespace DigitalFamilyCookbook.Data.Models;

public class AuthResult
{
    [JsonIgnore]
    public string RefreshToken { get; set; } = string.Empty;

    public string AccessToken { get; set; } = string.Empty;

    public bool IsSuccessful { get; set; }

    public string Error { get; set; } = string.Empty;
}