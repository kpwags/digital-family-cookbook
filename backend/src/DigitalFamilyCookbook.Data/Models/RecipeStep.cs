namespace DigitalFamilyCookbook.Data.Models
{
    public class RecipeStep
    {
        public int RecipeStepId { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; } = Recipe.None();

        public int StepId { get; set; }

        public Step Step { get; set; } = Step.None();
    }
}