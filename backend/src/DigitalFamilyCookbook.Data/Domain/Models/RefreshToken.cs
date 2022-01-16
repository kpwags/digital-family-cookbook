namespace DigitalFamilyCookbook.Data.Domain.Models;

public class RefreshToken
{
    public string Id { get; set; } = string.Empty;

    public int RefreshTokenId { get; set; }

    public string UserAccountId { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;

    public string JwtId { get; set; } = string.Empty;

    public bool IsUsed { get; set; }

    public bool IsRevoked { get; set; }

    public DateTime AddedDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public UserAccount UserAccount { get; set; } = new UserAccount();

    public static RefreshToken None() => new RefreshToken();

    public static RefreshToken FromDto(RefreshTokenDto dto)
    {
        return new RefreshToken
        {
            Id = dto.Id,
            RefreshTokenId = dto.RefreshTokenId,
            UserAccountId = dto.UserAccountId,
            Token = dto.Token,
            JwtId = dto.JwtId,
            IsUsed = dto.IsUsed,
            IsRevoked = dto.IsRevoked,
            AddedDate = dto.AddedDate,
            ExpirationDate = dto.ExpirationDate,
            UserAccount = UserAccount.FromDto(dto.UserAccount),
        };
    }
}