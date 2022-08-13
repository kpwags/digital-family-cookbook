namespace DigitalFamilyCookbook.Handlers.Queries.Recipes;

public class GetRecipesByUser
{
    public class Handler : IRequestHandler<Query, OperationResult<RecipeListPageResults>>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IFileService _fileService;

        public Handler(IRecipeRepository recipeRepository, IUserAccountRepository userAccountRepository, IFileService fileService)
        {
            _recipeRepository = recipeRepository;
            _userAccountRepository = userAccountRepository;
            _fileService = fileService;
        }

        public async Task<OperationResult<RecipeListPageResults>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var userAccount = await _userAccountRepository.GetUserAccountById(request.UserAccountId);

                var data = await Task.FromResult(_recipeRepository.GetUserRecipes(request.UserAccountId));

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

                return new OperationResult<RecipeListPageResults>(new RecipeListPageResults
                {
                    PageTitle = userAccount.Name,
                    Recipes = recipes,
                });
            }
            catch (Exception ex)
            {
                return new OperationResult<RecipeListPageResults>(ex.Message);
            }
        }
    }

    public class Query : IRequest<OperationResult<RecipeListPageResults>>
    {
        public string UserAccountId { get; set; } = string.Empty;

        public bool IncludeImages { get; set; }
    }
}