namespace DigitalFamilyCookbook.Handlers.Commands.Categories;

public class CreateMeat
{
    public class Handler : IRequestHandler<Command, OperationResult<string>>
    {
        private readonly IMeatRepository _meatRepository;

        public Handler(IMeatRepository meatRepository)
        {
            _meatRepository = meatRepository;
        }

        public async Task<OperationResult<string>> Handle(Command command, CancellationToken cancellationToken)
        {
            try
            {
                await _meatRepository.Add(new Meat
                {
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
        public string Name { get; set; } = string.Empty;
    }
}