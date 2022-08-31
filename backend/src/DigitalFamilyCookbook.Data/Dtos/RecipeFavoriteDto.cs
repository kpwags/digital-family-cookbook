namespace DigitalFamilyCookbook.Data.Dtos;

public class RecipeFavoriteDto : BaseDto
{
    public int RecipeFavoriteId { get; set; }

    public int RecipeId { get; set; }

    public RecipeDto Recipe { get; set; } = RecipeDto.None();

    public string UserAccountId { get; set; } = string.Empty;

    public UserAccountDto UserAccount { get; set; } = UserAccountDto.None();
}