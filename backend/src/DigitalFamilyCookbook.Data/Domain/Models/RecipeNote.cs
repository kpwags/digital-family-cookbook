namespace DigitalFamilyCookbook.Data.Domain.Models;

public class RecipeNote : BaseDomainModel
{
    public string Id { get; set; } = string.Empty;

    public int RecipeNoteId { get; set; }

    public int RecipeId { get; set; }

    public Recipe Recipe { get; set; } = Recipe.None();

    public int NoteId { get; set; }

    public Note Note { get; set; } = Note.None();

    public static RecipeNote None() => new RecipeNote();

    public static RecipeNote FromDto(RecipeNoteDto dto) => new RecipeNote
    {
        Id = dto.Id,
        RecipeNoteId = dto.RecipeNoteId,
        RecipeId = dto.RecipeId,
        Recipe = Recipe.FromDto(dto.Recipe),
        NoteId = dto.NoteId,
        Note = Note.FromDto(dto.Note),
        DateCreated = dto.DateCreated,
        DateUpdated = dto.DateUpdated,
    };
}
