namespace DigitalFamilyCookbook.Handlers.Queries.Recipes;

public class GetAllRecipes
{
    public class Handler : IRequestHandler<Query, OperationResult<IReadOnlyCollection<RecipeApiModel>>>
    {
        private readonly IRecipeRepository _recipeRepository;

        public Handler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<OperationResult<IReadOnlyCollection<RecipeApiModel>>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await Task.FromResult(_recipeRepository.GetAll());

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

    }
}