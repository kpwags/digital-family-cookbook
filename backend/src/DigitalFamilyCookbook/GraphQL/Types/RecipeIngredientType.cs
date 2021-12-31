namespace DigitalFamilyCookbook.GraphQL.Types;

public class RecipeIngredientType : ObjectGraphType<RecipeIngredient>
{
    public RecipeIngredientType()
    {
        Name = "RecipeIngredient";

        Field(ri => ri.Id, type: typeof(IdGraphType)).Description("The ID of the RecipeIngredient");
        Field(ri => ri.RecipeIngredientId, type: typeof(IntGraphType)).Description("The SQL ID of the RecipeIngredient");
        Field(ri => ri.RecipeId, type: typeof(IntGraphType)).Description("The ID of the recipe");
        Field(ri => ri.Recipe, type: typeof(RecipeType)).Description("The recipe");
        Field(ri => ri.IngredientId, type: typeof(IntGraphType)).Description("The ID of the ingredient");
        Field(ri => ri.Ingredient, type: typeof(IngredientType)).Description("The ingredient");
    }
}
