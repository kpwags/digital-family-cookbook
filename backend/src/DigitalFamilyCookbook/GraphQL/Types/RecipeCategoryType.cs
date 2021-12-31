using DigitalFamilyCookbook.Data.Models;
using GraphQL.Types;

namespace DigitalFamilyCookbook.GraphQL.Types
{
    public class RecipeCategoryType : ObjectGraphType<RecipeCategory>
    {
        public RecipeCategoryType()
        {
            Name = "RecipeCategory";

            Field(rc => rc.Id, type: typeof(IdGraphType)).Description("The ID of the RecipeCategory");
            Field(rc => rc.RecipeCategoryId, type: typeof(IntGraphType)).Description("The SQL ID of the RecipeCategory");
            Field(rc => rc.RecipeId, type: typeof(IntGraphType)).Description("The ID of the recipe");
            Field(rc => rc.Recipe, type: typeof(RecipeType)).Description("The recipe");
            Field(rc => rc.CategoryId, type: typeof(IntGraphType)).Description("The ID of the category");
            Field(rc => rc.Category, type: typeof(CategoryType)).Description("The category");
        }
    }
}