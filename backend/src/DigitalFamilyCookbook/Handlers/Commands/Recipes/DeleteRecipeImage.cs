namespace DigitalFamilyCookbook.Handlers.Commands.Recipes;

public class DeleteRecipeImage
{
    public class Handler : IRequestHandler<Command, OperationResult<string>>
    {
        private readonly IFileService _fileService;

        public Handler(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<OperationResult<string>> Handle(Command command, CancellationToken cancellationToken)
        {
            try
            {
                await Task.Run(() =>
                {
                    _fileService.DeleteRecipeImage(command.ImageFilename);
                });

                return new OperationResult<string>(true, "");
            }
            catch (Exception ex)
            {
                return new OperationResult<string>(false, "", ex.Message);
            }
        }
    }

    public class Command : IRequest<OperationResult<string>>
    {
        public string ImageFilename { get; set; } = string.Empty;
    }
}