namespace DigitalFamilyCookbook.Handlers.Commands.Recipes;

public class MarkRecipeAsFavorite
{
    public class Command : IRequest<Unit>
    {
        public string UserAccountId { get; set; } = string.Empty;

        public int RecipeId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IRecipeRepository _recipeRepository;

        public Handler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<Unit> Handle(Command cmd, CancellationToken cancellationToken)
        {
            await _recipeRepository.MarkRecipeAsFavorite(cmd.UserAccountId, cmd.RecipeId);

            return Unit.Value;
        }
    }
}
