namespace DigitalFamilyCookbook.Data.Dtos;

public class NoteDto : BaseDto
{
    public int NoteId { get; set; }

    public string NoteText { get; set; } = string.Empty;

    public List<RecipeNoteDto> RecipeNotes { get; set; } = new List<RecipeNoteDto>();

    public static NoteDto None() => new NoteDto();
}