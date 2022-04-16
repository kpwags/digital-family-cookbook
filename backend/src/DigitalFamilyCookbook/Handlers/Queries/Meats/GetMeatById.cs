namespace DigitalFamilyCookbook.Handlers.Queries.Categories;

public class GetMeatById
{
    public class Handler : IRequestHandler<Query, OperationResult<MeatApiModel>>
    {
        private readonly IMeatRepository _meatRepository;

        public Handler(IMeatRepository meatRepository)
        {
            _meatRepository = meatRepository;
        }

        public async Task<OperationResult<MeatApiModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var meat = await Task.FromResult(_meatRepository.Get(request.Id));

                return new OperationResult<MeatApiModel>(MeatApiModel.FromDomainModel(meat));
            }
            catch (Exception ex)
            {
                return new OperationResult<MeatApiModel>(ex.Message);
            }
        }
    }

    public class Query : IRequest<OperationResult<MeatApiModel>>
    {
        public int Id { get; set; }
    }
}