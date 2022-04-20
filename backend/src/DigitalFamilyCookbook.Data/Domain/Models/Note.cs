namespace DigitalFamilyCookbook.Data.Dtos;

public class Note : BaseDomainModel
{
    public string Id { get; set; } = string.Empty;

    public int NoteId { get; set; }

    public string NoteText { get; set; } = string.Empty;

    public IEnumerable<RecipeNote> RecipeNotes { get; set; } = Enumerable.Empty<RecipeNote>();

    public static Note None() => new Note();

    public static Note FromDto(NoteDto dto) => new Note
    {
        Id = dto.Id,
        NoteId = dto.NoteId,
        NoteText = dto.NoteText,
        DateCreated = dto.DateCreated,
        DateUpdated = dto.DateUpdated,
    };
}