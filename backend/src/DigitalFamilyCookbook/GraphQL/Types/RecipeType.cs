namespace DigitalFamilyCookbook.GraphQL.Types;

public class RecipeType : ObjectGraphType<Recipe>
{
    public RecipeType()
    {
        Name = "Recipe";

        Field(r => r.Id, type: typeof(IdGraphType)).Description("The ID of the recipe");
        Field(r => r.RecipeId, type: typeof(IntGraphType)).Description("The SQL ID of the recipe");
        Field(r => r.Name, type: typeof(StringGraphType)).Description("The name of the recipe");
        Field(r => r.Description, type: typeof(StringGraphType)).Description("The description of the recipe");
        Field(r => r.IsPublic, type: typeof(BooleanGraphType)).Description("Flag indicating whether the recipe is public");
        Field(r => r.Servings, type: typeof(IntGraphType)).Description("Number of servings the recipe makes");
        Field(r => r.Source, type: typeof(StringGraphType)).Description("Where the recipe was found");
        Field(r => r.SourceUrl, type: typeof(StringGraphType)).Description("The URL of the source (if applicable)");
        Field(r => r.Time, type: typeof(IntGraphType)).Description("The time (in minutes) it takes to cook");
        Field(r => r.ActiveTime, type: typeof(IntGraphType)).Description("The time (in minutes) it actively takes to cook");
        Field(r => r.ImageUrl, type: typeof(StringGraphType)).Description("The URL of the standard image");
        Field(r => r.ImageUrlLarge, type: typeof(StringGraphType)).Description("The URL of the large image");
        Field(r => r.Calories, type: typeof(FloatGraphType)).Description("The amount of calories (per serving) of the recipe");
        Field(r => r.Protein, type: typeof(FloatGraphType)).Description("The amount of protein (per serving) of the recipe");
        Field(r => r.Carbohydrates, type: typeof(FloatGraphType)).Description("The amount of carbohydrates (per serving) of the recipe");
        Field(r => r.Fat, type: typeof(FloatGraphType)).Description("The amount of fat (per serving) of the recipe");
        Field(r => r.Sugar, type: typeof(FloatGraphType)).Description("The amount of sugar (per serving) of the recipe");
        Field(r => r.Fiber, type: typeof(FloatGraphType)).Description("The amount of fiber (per serving) of the recipe");
        Field(r => r.Cholesterol, type: typeof(FloatGraphType)).Description("The amount of cholesterol (per serving) of the recipe");

        Field(r => r.Ingredients, type: typeof(ListGraphType<IngredientType>)).Description("The collection of Ingredients");

        Field(r => r.Steps, type: typeof(ListGraphType<StepType>)).Description("The collection of Steps");

        Field(r => r.Meats, type: typeof(ListGraphType<MeatType>)).Description("The collection of Meats");

        Field(r => r.Categories, type: typeof(ListGraphType<CategoryType>)).Description("The collection of Categories");

        Field(r => r.UserAccount, type: typeof(UserAccountType)).Description("The user account of the user who created the recipe");
    }
}
