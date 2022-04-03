namespace DigitalFamilyCookbook.Handlers.Queries.Categories;

public class GetCategoryById
{
    public class Handler : IRequestHandler<Query, OperationResult<CategoryApiModel>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public Handler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult<CategoryApiModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await Task.FromResult(_categoryRepository.Get(request.Id));

                return new OperationResult<CategoryApiModel>(CategoryApiModel.FromDomainModel(category));
            }
            catch (Exception ex)
            {
                return new OperationResult<CategoryApiModel>(ex.Message);
            }
        }
    }

    public class Query : IRequest<OperationResult<CategoryApiModel>>
    {
        public int Id { get; set; }
    }
}