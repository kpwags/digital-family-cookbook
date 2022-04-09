namespace DigitalFamilyCookbook.Handlers.Commands.Categories;

public class CreateCategory
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
                await _categoryRepository.Add(new Category
                {
                    Name = command.Name,
                });
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
        public string Name { get; set; } = string.Empty;
    }
}