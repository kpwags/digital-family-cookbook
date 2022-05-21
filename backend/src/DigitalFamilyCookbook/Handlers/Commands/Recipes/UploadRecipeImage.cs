namespace DigitalFamilyCookbook.Handlers.Commands.Recipes;

public class UploadRecipeImage
{
    public class Handler : IRequestHandler<Command, OperationResult<string>>
    {
        private readonly IRecipeRepository _recipeRepository;

        public Handler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<OperationResult<string>> Handle(Command command, CancellationToken cancellationToken)
        {
            try
            {
                // await _recipeRepository.Delete(command.Id);
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
        public IFormFile? Image { get; set; }
    }
}