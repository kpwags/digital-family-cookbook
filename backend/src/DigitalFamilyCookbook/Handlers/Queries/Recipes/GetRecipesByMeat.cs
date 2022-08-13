namespace DigitalFamilyCookbook.Handlers.Queries.Recipes;

public class GetRecipesByMeat
{
    public class Handler : IRequestHandler<Query, OperationResult<RecipeListPageResults>>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMeatRepository _meatRepository;
        private readonly IFileService _fileService;

        public Handler(IRecipeRepository recipeRepository, IMeatRepository meatRepository, IFileService fileService)
        {
            _recipeRepository = recipeRepository;
            _meatRepository = meatRepository;
            _fileService = fileService;
        }

        public async Task<OperationResult<RecipeListPageResults>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var meat = _meatRepository.Get(request.MeatId);

                var data = await Task.FromResult(_recipeRepository.GetRecipesForMeat(request.MeatId));

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
                    PageTitle = meat.Name,
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
        public int MeatId { get; set; }

        public bool IncludeImages { get; set; }
    }
}