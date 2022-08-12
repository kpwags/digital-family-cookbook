namespace DigitalFamilyCookbook.Handlers.Queries.Recipes;

public class GetRecipesByCategory
{
    public class Handler : IRequestHandler<Query, OperationResult<RecipeListPageResults>>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ICategoryRepository _categoryRepository;

        public Handler(IRecipeRepository recipeRepository, ICategoryRepository categoryRepository)
        {
            _recipeRepository = recipeRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult<RecipeListPageResults>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var category = _categoryRepository.Get(request.CategoryId);

                var data = await Task.FromResult(_recipeRepository.GetRecipesForCategory(request.CategoryId));

                var recipes = data
                    .Select(r => RecipeApiModel.FromDomainModel(r))
                    .OrderBy(r => r.Name)
                    .ToList();

                return new OperationResult<RecipeListPageResults>(new RecipeListPageResults
                {
                    PageTitle = category.Name,
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
        public int CategoryId { get; set; }
    }
}