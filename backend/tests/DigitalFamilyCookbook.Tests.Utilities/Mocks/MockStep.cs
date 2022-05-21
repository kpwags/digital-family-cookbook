namespace DigitalFamilyCookbook.Tests.Utilities.Mocks;

public static class MockStep
{
    public static StepDto GenerateDto(int? sortOrder = null) => new StepDto
    {
        StepId = MockDataGenerator.RandomInteger(),
        Id = MockDataGenerator.RandomId(),
        Direction = MockDataGenerator.RandomString(200),
        SortOrder = sortOrder.HasValue ? sortOrder.Value : MockDataGenerator.RandomInteger(),
    };

    public static Step GenerateDomainModel(int? sortOrder = null) => Step.FromDto(GenerateDto(sortOrder));

    public static StepApiModel GenerateApiModel(int? sortOrder = null) => StepApiModel.FromDomainModel(GenerateDomainModel(sortOrder));

    public static List<StepDto> GenerateForRecipeDto(int stepCount = 5)
    {
        var steps = new List<StepDto>();

        for (var i = 0; i < stepCount; i += 1)
        {
            steps.Add(GenerateDto(i + 1));
        }

        return steps;
    }

    public static List<Step> GenerateForRecipe(int stepCount = 5)
    {
        var steps = new List<Step>();

        for (var i = 0; i < stepCount; i += 1)
        {
            steps.Add(GenerateDomainModel(i + 1));
        }

        return steps;
    }

    public static List<StepApiModel> GenerateForRecipeApiModel(int stepCount = 5)
    {
        var steps = new List<StepApiModel>();

        for (var i = 0; i < stepCount; i += 1)
        {
            steps.Add(GenerateApiModel(i + 1));
        }

        return steps;
    }
}