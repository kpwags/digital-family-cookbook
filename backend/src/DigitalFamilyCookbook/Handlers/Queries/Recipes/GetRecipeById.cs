namespace DigitalFamilyCookbook.Handlers.Queries.Recipes;

public class GetRecipeById
{
    public class Handler : IRequestHandler<Query, OperationResult<RecipeApiModel>>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ICategoryRepository _categoryRepostory;
        private readonly IMeatRepository _meatRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Handler(
            IRecipeRepository recipeRepository,
            ICategoryRepository categoryRepository,
            IMeatRepository meatRepository,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor)
        {
            _recipeRepository = recipeRepository;
            _categoryRepostory = categoryRepository;
            _meatRepository = meatRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OperationResult<RecipeApiModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var recipe = await Task.FromResult(_recipeRepository.GetById(request.Id));

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

                recipe.Categories = _categoryRepostory.GetForRecipe(recipe.RecipeId);
                recipe.Meats = _meatRepository.GetForRecipe(recipe.RecipeId);

                var user = _httpContextAccessor.HttpContext?.CurrentUser(false);

                if (user is not null)
                {
                    recipe.IsFavorite = _recipeRepository.IsRecipeFavoriteForUser(user.Id, request.Id);
                }

                return new OperationResult<RecipeApiModel>(RecipeApiModel.FromDomainModel(recipe));
            }
            catch (Exception ex)
            {
                return new OperationResult<RecipeApiModel>(ex.Message);
            }
        }
    }

    public class Query : IRequest<OperationResult<RecipeApiModel>>
    {
        public int Id { get; set; }
    }
}