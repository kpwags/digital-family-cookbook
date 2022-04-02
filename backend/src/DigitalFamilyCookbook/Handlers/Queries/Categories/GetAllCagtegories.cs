namespace DigitalFamilyCookbook.Handlers.Queries.Categories;

public class GetAllCategories
{
    public class Handler : IRequestHandler<Query, OperationResult<IReadOnlyCollection<CategoryApiModel>>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public Handler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult<IReadOnlyCollection<CategoryApiModel>>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await Task.FromResult(_categoryRepository.GetAllCategories());

                var categories = data
                    .Select(c => CategoryApiModel.FromDomainModel(c))
                    .OrderBy(c => c.Name)
                    .ToList();

                return new OperationResult<IReadOnlyCollection<CategoryApiModel>>(categories);
            }
            catch (Exception ex)
            {
                return new OperationResult<IReadOnlyCollection<CategoryApiModel>>(ex.Message);
            }
        }
    }

    public class Query : IRequest<OperationResult<IReadOnlyCollection<CategoryApiModel>>>
    {

    }
}