namespace DigitalFamilyCookbook.Data.Domain.Models;

public class RecipeFavorite : BaseDomainModel
{
    public string Id { get; set; } = string.Empty;

    public int RecipeFavoriteId { get; set; }

    public int RecipeId { get; set; }

    public Recipe Recipe { get; set; } = Recipe.None();

    public string UserAccountId { get; set; } = string.Empty;

    public UserAccount User { get; set; } = UserAccount.None();

    public static RecipeFavorite None() => new RecipeFavorite();

    public static RecipeFavorite FromDto(RecipeFavoriteDto dto) => new RecipeFavorite
    {
        Id = dto.Id,
        RecipeFavoriteId = dto.RecipeFavoriteId,
        RecipeId = dto.RecipeId,
        Recipe = Recipe.FromDto(dto.Recipe),
        UserAccountId = dto.UserAccountId,
        User = UserAccount.FromDto(dto.UserAccount),
        DateCreated = dto.DateCreated,
        DateUpdated = dto.DateUpdated,
    };
}
