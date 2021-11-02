using DigitalFamilyCookbook.Data.Models;
using GraphQL.Types;

namespace DigitalFamilyCookbook.GraphQL
{
    public class IngredientType : ObjectGraphType<Ingredient>
    {
        public IngredientType()
        {
            Name = "Ingredient";

            Field(i => i.Id, type: typeof(IdGraphType)).Description("The ID of the Ingredient");
            Field(i => i.IngredientId, type: typeof(IntGraphType)).Description("The SQL ID of the Ingredient");
            Field(i => i.Name, type: typeof(StringGraphType)).Description("The Name of the Ingredient");
            Field(i => i.SortOrder, type: typeof(IntGraphType)).Description("The Order of the Ingredient");
        }
    }
}