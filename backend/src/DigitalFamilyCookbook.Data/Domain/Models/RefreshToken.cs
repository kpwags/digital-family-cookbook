using System.Text.Json.Serialization;

namespace DigitalFamilyCookbook.Data.Domain.Models;

public class RefreshToken : BaseDomainModel
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Token { get; set; } = string.Empty;

    public DateTime Expires { get; set; }

    public string CreatedByIp { get; set; } = string.Empty;

    public DateTime? Revoked { get; set; }

    public string RevokedByIp { get; set; } = string.Empty;

    public string? ReplacedByToken { get; set; } = string.Empty;

    public string? ReasonRevoked { get; set; } = string.Empty;

    public UserAccount UserAccount { get; set; } = UserAccount.None();

    public bool IsExpired => DateTime.UtcNow >= Expires;

    public bool IsRevoked => Revoked != null;

    public bool IsActive => !IsRevoked && !IsExpired;

    public static RefreshToken FromDto(RefreshTokenDto dto) => new RefreshToken
    {
        Id = dto.RefreshTokenId,
        Token = dto.Token,
        Expires = dto.Expires,
        CreatedByIp = dto.CreatedByIp,
        Revoked = dto.Revoked,
        RevokedByIp = dto.RevokedByIp,
        ReplacedByToken = dto.ReplacedByToken,
        ReasonRevoked = dto.ReasonRevoked,
        DateCreated = dto.DateCreated,
        DateUpdated = dto.DateUpdated,
        UserAccount = UserAccount.FromDto(dto.UserAccount),
    };
}
