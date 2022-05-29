namespace DigitalFamilyCookbook.Handlers.Commands.Recipes;

public class UpdateRecipe
{
    public class Handler : IRequestHandler<Command, OperationResult<string>>
    {
        private readonly IRecipeRepository _recipeRepository;

        public Handler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<OperationResult<string>> Handle(Command command, CancellationToken cancellationToken)
        {
            try
            {
                await _recipeRepository.Update(new Recipe
                {
                    RecipeId = command.RecipeId,
                    Name = command.Name,
                    Description = command.Description,
                    IsPublic = command.IsPublic,
                    Servings = command.Servings,
                    Source = command.Source,
                    SourceUrl = command.SourceUrl,
                    Time = command.Time,
                    ActiveTime = command.ActiveTime,
                    ImageUrl = command.ImageFilename != "" ? $"{command.ImageFilename}_sm.jpg" : string.Empty,
                    ImageUrlLarge = command.ImageFilename != "" ? $"{command.ImageFilename}.jpg" : string.Empty,
                    Calories = command.Calories,
                    Carbohydrates = command.Carbohydrates,
                    Sugar = command.Sugar,
                    Fat = command.Fat,
                    Protein = command.Protein,
                    Fiber = command.Fiber,
                    Cholesterol = command.Cholesterol,
                    Categories = command.Categories.Select(c => new Category { CategoryId = c }),
                    Meats = command.Meats.Select(m => new Meat { MeatId = m }),
                    Ingredients = command.Ingredients.Select(i => new Ingredient { Name = i.Name, SortOrder = i.SortOrder }),
                    Steps = command.Steps.Select(i => new Step { Direction = i.Name, SortOrder = i.SortOrder }),
                });

                return new OperationResult<string>(true, "", "");
            }
            catch (Exception ex)
            {
                return new OperationResult<string>(false, "", ex.Message);
            }
        }
    }

    public class Command : IRequest<OperationResult<string>>
    {
        public int RecipeId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public bool IsPublic { get; set; }

        public int Servings { get; set; }

        public string Source { get; set; } = string.Empty;

        public string SourceUrl { get; set; } = string.Empty;

        public int? Time { get; set; }

        public int? ActiveTime { get; set; }

        public string ImageFilename { get; set; } = string.Empty;

        public decimal? Calories { get; set; }

        public decimal? Carbohydrates { get; set; }

        public decimal? Sugar { get; set; }

        public decimal? Fat { get; set; }

        public decimal? Protein { get; set; }

        public decimal? Fiber { get; set; }

        public decimal? Cholesterol { get; set; }

        public List<int> Categories { get; set; } = new List<int>();

        public List<int> Meats { get; set; } = new List<int>();

        public List<DigitalFamilyCookbook.Models.RecipeStep> Ingredients { get; set; } = new List<DigitalFamilyCookbook.Models.RecipeStep>();

        public List<DigitalFamilyCookbook.Models.RecipeStep> Steps { get; set; } = new List<DigitalFamilyCookbook.Models.RecipeStep>();
    }
}