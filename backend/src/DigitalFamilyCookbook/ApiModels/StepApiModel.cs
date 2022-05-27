namespace DigitalFamilyCookbook.ApiModels;

public class StepApiModel : BaseApiModel
{
    public string Id { get; set; } = string.Empty;

    public int StepId { get; set; }

    public string Direction { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    public static StepApiModel None() => new StepApiModel();

    public static StepApiModel FromDomainModel(Step step)
    {
        return new StepApiModel
        {
            Id = step.Id,
            StepId = step.StepId,
            Direction = step.Direction,
            SortOrder = step.SortOrder,
            DateCreated = step.DateCreated,
            DateUpdated = step.DateUpdated,
        };
    }
}
