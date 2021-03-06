namespace DigitalFamilyCookbook.GraphQL.Types;

public class IngredientType : ObjectGraphType<Ingredient>
{
    public IngredientType()
    {
        Name = "Ingredient";

        Field(i => i.Id, type: typeof(IdGraphType)).Description("The ID of the Ingredient");
        Field(i => i.IngredientId, type: typeof(IntGraphType)).Description("The SQL ID of the Ingredient");
        Field(i => i.RecipeId, type: typeof(IntGraphType)).Description("The SQL ID of the Recipe");
        Field(i => i.Name, type: typeof(StringGraphType)).Description("The Name of the Ingredient");
        Field(i => i.SortOrder, type: typeof(IntGraphType)).Description("The Order of the Ingredient");
    }
}
