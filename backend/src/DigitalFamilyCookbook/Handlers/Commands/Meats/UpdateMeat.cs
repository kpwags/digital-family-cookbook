namespace DigitalFamilyCookbook.Handlers.Commands.Categories;

public class UpdateMeat
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
            if (command.Id == 0)
            {
                return new OperationResult<string>(false, "", "Unable to find meat");
            }

            try
            {
                await _meatRepository.Update(new Meat
                {
                    MeatId = command.Id,
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