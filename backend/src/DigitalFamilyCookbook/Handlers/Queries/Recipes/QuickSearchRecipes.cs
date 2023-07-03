namespace DigitalFamilyCookbook.Handlers.Queries.Recipes;

public class QuickSearchRecipes
{
    public class Handler : IRequestHandler<Query, IReadOnlyCollection<RecipeApiModel>>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Handler(IRecipeRepository recipeRepository, IHttpContextAccessor httpContextAccessor)
        {
            _recipeRepository = recipeRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IReadOnlyCollection<RecipeApiModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Keywords.Trim() == string.Empty)
                {
                    throw new Exception("No search keywords provided");
                }

                var includePrivateRecipes = _httpContextAccessor.HttpContext?.IsUserLoggedIn() ?? false;
                
                var recipes = await Task.FromResult(_recipeRepository.QuickSearchRecipes(request.Keywords, includePrivateRecipes, request.MaxRecipes));

                return recipes
                    .Select(RecipeApiModel.FromDomainModel)
                    .OrderBy(r => r.Name)
                    .ToList();
            }
            catch
            {
                return new List<RecipeApiModel>();
            }
        }
    }

    public class Query : IRequest<IReadOnlyCollection<RecipeApiModel>>
    {
        public string Keywords { get; set; } = string.Empty;

        public int MaxRecipes { get; set; } = 10;
    }
}