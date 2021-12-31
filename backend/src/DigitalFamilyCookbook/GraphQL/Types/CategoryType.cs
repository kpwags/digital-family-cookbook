namespace DigitalFamilyCookbook.GraphQL.Types;

public class CategoryType : ObjectGraphType<Category>
{
    public CategoryType()
    {
        Name = "Category";

        Field(c => c.Id, type: typeof(IdGraphType)).Description("The ID of the Category");
        Field(c => c.CategoryId, type: typeof(IntGraphType)).Description("The SQL ID of the Category");
        Field(c => c.Name, type: typeof(StringGraphType)).Description("The Name of the Category");
    }
}
