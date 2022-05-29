namespace DigitalFamilyCookbook.Handlers.Commands.Recipes;

public class DeleteRecipeImage
{
    public class Handler : IRequestHandler<Command, OperationResult<string>>
    {
        private readonly IFileService _fileService;
        private readonly IRecipeRepository _recipeRepository;

        public Handler(IFileService fileService, IRecipeRepository recipeRepository)
        {
            _fileService = fileService;
            _recipeRepository = recipeRepository;
        }

        public async Task<OperationResult<string>> Handle(Command command, CancellationToken cancellationToken)
        {
            try
            {
                _fileService.DeleteRecipeImage(command.ImageFilename);

                if (command.RecipeId > 0)
                {
                    await _recipeRepository.DeleteRecipeImage(command.RecipeId);
                }

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

        public int RecipeId { get; set; }
    }
}