namespace DigitalFamilyCookbook.Data.Dtos;

public class RecipeNoteDto : BaseDto
{
    public string Id { get; set; } = string.Empty;

    public int RecipeNoteId { get; set; }

    public int RecipeId { get; set; }

    public RecipeDto Recipe { get; set; } = RecipeDto.None();

    public int NoteId { get; set; }

    public NoteDto Note { get; set; } = NoteDto.None();
}
