namespace DigitalFamilyCookbook.Handlers.Commands.Categories;

public class UpdateCategory
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
            if (command.Id == 0)
            {
                return new OperationResult<string>(false, "", "Unable to find category");
            }

            try
            {
                await _categoryRepository.Update(new Category
                {
                    CategoryId = command.Id,
                    Name = command.Name.Trim(),
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
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}