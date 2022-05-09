namespace DigitalFamilyCookbook.Data.Repositories;

public interface IStepRepository
{
    Task<Step> Add(Step step);

    Task Delete(int stepId);

    Task DeleteRange(List<int> stepIds);

    Task DeleteForRecipe(int recipeId);
}