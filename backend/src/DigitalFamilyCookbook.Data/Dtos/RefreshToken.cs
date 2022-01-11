namespace DigitalFamilyCookbook.Data.Dtos;

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
}