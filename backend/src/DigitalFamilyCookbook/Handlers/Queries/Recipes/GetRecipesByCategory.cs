using DigitalFamilyCookbook.Core.Configuration;

namespace DigitalFamilyCookbook.Handlers.Queries.Recipes;

public class GetRecipesByCategory
{
    public class Handler : IRequestHandler<Query, OperationResult<RecipeListPageResults>>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Handler(
            IRecipeRepository recipeRepository,
            ICategoryRepository categoryRepository,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _recipeRepository = recipeRepository;
            _categoryRepository = categoryRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OperationResult<RecipeListPageResults>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var includePrivateRecipes = _httpContextAccessor.HttpContext?.IsUserLoggedIn() ?? false;
                var category = _categoryRepository.Get(request.CategoryId);

                var (data, totalRecipes) = await Task.FromResult(_recipeRepository.GetRecipesForCategoryPaginated(request.CategoryId, includePrivateRecipes, request.PageNumber, request.RecipesPerPage));

                var recipes = data
                    .Select(RecipeApiModel.FromDomainModel)
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
                    PageTitle = category.Name,
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

    public class Query : IRequest<OperationResult<RecipeListPageResults>>
    {
        public int CategoryId { get; set; }

        public int PageNumber { get; set; } = 1;

        public bool IncludeImages { get; set; }

        public int RecipesPerPage { get; set; } = 10;
    }
}