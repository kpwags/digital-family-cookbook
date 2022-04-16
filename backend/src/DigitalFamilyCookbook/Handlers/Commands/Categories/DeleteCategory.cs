namespace DigitalFamilyCookbook.Handlers.Commands.Categories;

public class DeleteCategory
{
    public class Handler : IRequestHandler<Command, OperationResult<string>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public Handler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult<string>> Handle(Command command, CancellationToken cancellationToken)
        {
            try
            {
                await _categoryRepository.Delete(command.Id);
            }
            catch (Exception ex)
            {
                return new OperationResult<string>(false, "", ex.Message);
            }

            return new OperationResult<string>(true, "");
        }
    }

    public class Command : IRequest<OperationResult<string>>
    {
        public int Id { get; set; }
    }
}