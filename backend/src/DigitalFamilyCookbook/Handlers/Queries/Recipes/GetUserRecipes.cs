namespace DigitalFamilyCookbook.Handlers.Queries.Recipes;

public class GetUserRecipes
{
    public class Handler : IRequestHandler<Query, OperationResult<IReadOnlyCollection<RecipeApiModel>>>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Handler(IRecipeRepository recipeRepository, IHttpContextAccessor httpContextAccessor)
        {
            _recipeRepository = recipeRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OperationResult<IReadOnlyCollection<RecipeApiModel>>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var includePrivateRecipes = _httpContextAccessor.HttpContext?.IsUserLoggedIn() ?? false;
                var userAccountId = request.UserAccountId;

                if (userAccountId == string.Empty)
                {
                    var user = _httpContextAccessor.HttpContext?.CurrentUser();

                    if (user is null)
                    {
                        throw new Exception("Unable to identify user");
                    }

                    userAccountId = user.Id;
                }

                var data = await Task.FromResult(_recipeRepository.GetRecipesForUser(userAccountId, includePrivateRecipes));

                var recipes = data
                    .Select(r => RecipeApiModel.FromDomainModel(r))
                    .OrderBy(r => r.Name)
                    .ToList();

                return new OperationResult<IReadOnlyCollection<RecipeApiModel>>(recipes);
            }
            catch (Exception ex)
            {
                return new OperationResult<IReadOnlyCollection<RecipeApiModel>>(ex.Message);
            }
        }
    }

    public class Query : IRequest<OperationResult<IReadOnlyCollection<RecipeApiModel>>>
    {
        public string UserAccountId { get; set; } = string.Empty;
    }
}