namespace DigitalFamilyCookbook.Handlers.Queries.Recipes;

public class GetRecipeFavorites
{
    public class Query : IRequest<OperationResult<RecipeListPageResults>>
    {
        public string UserAccountId { get; set; } = string.Empty;

        public int PageNumber { get; set; } = 1;

        public bool IncludeImages { get; set; }

        public int RecipesPerPage { get; set; } = 10;
    }

    public class Handler : IRequestHandler<Query, OperationResult<RecipeListPageResults>>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;

        public Handler(IRecipeRepository recipeRepository, IHttpContextAccessor httpContextAccessor, IFileService fileService)
        {
            _recipeRepository = recipeRepository;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
        }

        public async Task<OperationResult<RecipeListPageResults>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
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

                var (data, totalRecipes) = await Task.FromResult(_recipeRepository.GetFavoriteRecipesForUserPaginated(userAccountId, request.PageNumber, request.RecipesPerPage));

                var recipes = data
                    .Select(r => RecipeApiModel.FromDomainModel(r))
                    .OrderBy(r => r.Name)
                    .ToList();

                if (request.IncludeImages)
                {
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
                }

                var maxPage = (decimal)totalRecipes / request.RecipesPerPage;
                var pageCount = (int)Math.Ceiling((double)maxPage);

                return new OperationResult<RecipeListPageResults>(new RecipeListPageResults
                {
                    PageTitle = "Favorite Recipes",
                    Recipes = recipes,
                    PageCount = pageCount,
                    TotalRecipeCount = totalRecipes,
                });
            }
            catch (Exception ex)
            {
                return new OperationResult<RecipeListPageResults>(ex.Message);
            }
        }
    }
}