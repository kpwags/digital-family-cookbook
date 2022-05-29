namespace DigitalFamilyCookbook.Handlers.Commands.Recipes;

public class CreateRecipe
{
    public class Handler : IRequestHandler<Command, OperationResult<RecipeApiModel>>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Handler(IRecipeRepository recipeRepository, IHttpContextAccessor httpContextAccessor)
        {
            _recipeRepository = recipeRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OperationResult<RecipeApiModel>> Handle(Command command, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext?.CurrentUser();

            if (user is null)
            {
                throw new Exception("Unable to identify user");
            }

            try
            {
                var recipe = await _recipeRepository.Add(new Recipe
                {
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
                    UserAccount = user.ToDomainModel(),
                });

                return new OperationResult<RecipeApiModel>(RecipeApiModel.FromDomainModel(recipe));
            }
            catch (Exception ex)
            {
                return new OperationResult<RecipeApiModel>(ex.Message);
            }
        }
    }

    public class Command : IRequest<OperationResult<RecipeApiModel>>
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public bool IsPublic { get; set; }

        public int Servings { get; set; }

        public string Source { get; set; } = string.Empty;

        public string SourceUrl { get; set; } = string.Empty;

        public int? Time { get; set; }

        public int? ActiveTime { get; set; }

        public decimal? Calories { get; set; }

        public decimal? Carbohydrates { get; set; }

        public decimal? Sugar { get; set; }

        public decimal? Fat { get; set; }

        public decimal? Protein { get; set; }

        public decimal? Fiber { get; set; }

        public decimal? Cholesterol { get; set; }

        public string ImageFilename { get; set; } = string.Empty;

        public List<int> Categories { get; set; } = new List<int>();

        public List<int> Meats { get; set; } = new List<int>();

        public List<DigitalFamilyCookbook.Models.RecipeStep> Ingredients { get; set; } = new List<DigitalFamilyCookbook.Models.RecipeStep>();

        public List<DigitalFamilyCookbook.Models.RecipeStep> Steps { get; set; } = new List<DigitalFamilyCookbook.Models.RecipeStep>();
    }
}