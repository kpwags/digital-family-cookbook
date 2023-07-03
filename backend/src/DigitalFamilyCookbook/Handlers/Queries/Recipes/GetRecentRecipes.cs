namespace DigitalFamilyCookbook.Handlers.Queries.Recipes;

public class GetRecentRecipes
{
    public class Handler : IRequestHandler<Query, IReadOnlyCollection<RecipeApiModel>>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Handler(IRecipeRepository recipeRepository, IFileService fileService, IHttpContextAccessor httpContextAccessor)    
        {
            _recipeRepository = recipeRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IReadOnlyCollection<RecipeApiModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            var includePrivateRecipes = _httpContextAccessor.HttpContext?.IsUserLoggedIn() ?? false;
            var data = await Task.FromResult(_recipeRepository.GetRecentRecipes(request.Count, includePrivateRecipes));

            var recipes = data
                .Select(RecipeApiModel.FromDomainModel)
                .OrderBy(r => r.Name)
                .ToList();

            if (!request.IncludeImages)
            {
                return recipes;
            }

            foreach (var recipe in recipes)
            {
                if (recipe.ImageUrl?.Length > 0)
                {
                    try
                    {
                        // don't fail the whole process if the image can't be found
                        recipe.ImageData = _fileService.GetRecipeImage(recipe.ImageUrl);
                    }
                    catch { }
                }

                if (recipe.ImageUrlLarge?.Length > 0)
                {
                    try
                    {
                        // don't fail the whole process if the image can't be found
                        recipe.LargeImageData = _fileService.GetRecipeImage(recipe.ImageUrlLarge);
                    }
                    catch { }
                }
            }

            return recipes;
        }
    }

    public class Query : IRequest<IReadOnlyCollection<RecipeApiModel>>
    {
        public int Count { get; set; } = 8;

        public bool IncludeImages { get; set; }
    }
}