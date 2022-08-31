using System.Text.Json.Serialization;

namespace DigitalFamilyCookbook.Data.Dtos;

public class RefreshTokenDto : BaseDto
{
    [JsonIgnore]
    public int RefreshTokenId { get; set; }

    public string Token { get; set; } = string.Empty;

    public DateTime Expires { get; set; }

    public string CreatedByIp { get; set; } = string.Empty;

    public DateTime? Revoked { get; set; }

    public string RevokedByIp { get; set; } = string.Empty;

    public string? ReplacedByToken { get; set; } = string.Empty;

    public string? ReasonRevoked { get; set; } = string.Empty;

    public UserAccountDto UserAccount { get; set; } = UserAccountDto.None();
}
