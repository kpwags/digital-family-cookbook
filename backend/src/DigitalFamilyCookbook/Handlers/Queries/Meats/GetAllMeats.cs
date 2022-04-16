namespace DigitalFamilyCookbook.Handlers.Queries.Categories;

public class GetAllMeats
{
    public class Handler : IRequestHandler<Query, OperationResult<IReadOnlyCollection<MeatApiModel>>>
    {
        private readonly IMeatRepository _meatRepository;

        public Handler(IMeatRepository meatRepository)
        {
            _meatRepository = meatRepository;
        }

        public async Task<OperationResult<IReadOnlyCollection<MeatApiModel>>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await Task.FromResult(_meatRepository.GetAll());

                var meats = data
                    .Select(m => MeatApiModel.FromDomainModel(m))
                    .OrderBy(m => m.Name)
                    .ToList();

                return new OperationResult<IReadOnlyCollection<MeatApiModel>>(meats);
            }
            catch (Exception ex)
            {
                return new OperationResult<IReadOnlyCollection<MeatApiModel>>(ex.Message);
            }
        }
    }

    public class Query : IRequest<OperationResult<IReadOnlyCollection<MeatApiModel>>>
    {

    }
}