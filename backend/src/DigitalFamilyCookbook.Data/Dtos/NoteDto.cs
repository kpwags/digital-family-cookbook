namespace DigitalFamilyCookbook.Data.Dtos;

public class NoteDto : BaseDto
{
    public string Id { get; set; } = string.Empty;

    public int NoteId { get; set; }

    public string NoteText { get; set; } = string.Empty;

    public List<RecipeNoteDto> RecipeNotes { get; set; } = new List<RecipeNoteDto>();

    public static NoteDto None() => new NoteDto();
}