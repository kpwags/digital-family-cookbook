using DigitalFamilyCookbook.Data.Models;
using GraphQL.Types;

namespace DigitalFamilyCookbook.GraphQL.Types
{
    public class RecipeMeatType : ObjectGraphType<RecipeMeat>
    {
        public RecipeMeatType()
        {
            Name = "RecipeMeat";

            Field(rm => rm.Id, type: typeof(IdGraphType)).Description("The ID of the RecipeMeat");
            Field(rm => rm.RecipeMeatId, type: typeof(IntGraphType)).Description("The SQL ID of the RecipeMeat");
            Field(rm => rm.RecipeId, type: typeof(IntGraphType)).Description("The ID of the recipe");
            Field(rm => rm.Recipe, type: typeof(RecipeType)).Description("The recipe");
            Field(rm => rm.MeatId, type: typeof(IntGraphType)).Description("The ID of the meat");
            Field(rm => rm.Meat, type: typeof(MeatType)).Description("The meat");
        }
    }
}