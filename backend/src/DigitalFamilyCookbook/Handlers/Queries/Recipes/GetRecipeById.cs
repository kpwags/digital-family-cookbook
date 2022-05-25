namespace DigitalFamilyCookbook.Handlers.Queries.Recipes;

public class GetRecipeById
{
    public class Handler : IRequestHandler<Query, OperationResult<RecipeApiModel>>
    {
        private readonly IRecipeRepository _recipeRepository;

        public Handler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<OperationResult<RecipeApiModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var recipe = await Task.FromResult(_recipeRepository.GetById(request.Id));

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