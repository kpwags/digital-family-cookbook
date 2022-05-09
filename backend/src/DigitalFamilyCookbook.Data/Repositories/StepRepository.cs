namespace DigitalFamilyCookbook.Data.Repositories;

public class StepRepository : IStepRepository
{
    private readonly ApplicationDbContext _db;

    public StepRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Step> Add(Step step)
    {
        var dto = new StepDto
        {
            Id = Guid.NewGuid().ToString(),
            Direction = step.Direction,
            SortOrder = step.SortOrder,
            RecipeId = step.RecipeId,
        };

        _db.Steps.Add(dto);

        await _db.SaveChangesAsync();

        return Step.FromDto(dto);
    }

    public async Task Delete(int stepId)
    {
        var step = _db.Steps.FirstOrDefault(s => s.StepId == stepId);

        if (step is null)
        {
            throw new Exception("Direction not found");
        }

        _db.Steps.Remove(step);

        await _db.SaveChangesAsync();
    }

    public async Task DeleteRange(List<int> stepIds)
    {
        var steps = _db.Steps.Where(s => stepIds.Contains(s.StepId));

        _db.Steps.RemoveRange(steps);

        await _db.SaveChangesAsync();
    }

    public async Task DeleteForRecipe(int recipeId)
    {
        var steps = _db.Steps.Where(s => s.RecipeId == recipeId);

        _db.Steps.RemoveRange(steps);

        await _db.SaveChangesAsync();
    }
}