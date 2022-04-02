namespace DigitalFamilyCookbook.Handlers.Commands.Categories;

public class SaveCategory
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
                if (command.Id == 0)
                {
                    await _categoryRepository.Add(new Category
                    {
                        Name = command.Name,
                    });
                }
                else
                {
                    await _categoryRepository.Update(new Category
                    {
                        CategoryId = command.Id,
                        Name = command.Name,
                    });
                }
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