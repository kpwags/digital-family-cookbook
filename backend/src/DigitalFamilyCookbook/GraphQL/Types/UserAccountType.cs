namespace DigitalFamilyCookbook.GraphQL.Types;

public class UserAccountType : ObjectGraphType<UserAccount>
{
    public UserAccountType()
    {
        Name = "UserAccount";

        Field(ua => ua.UserId, type: typeof(IdGraphType)).Description("The ID of the user account");
        Field(ua => ua.Id, type: typeof(StringGraphType)).Description("The SQL ID of the user account");
        Field(ua => ua.Name, type: typeof(StringGraphType)).Description("The name of the user");
        Field(ua => ua.Recipes, type: typeof(ListGraphType<RecipeType>)).Description("The recipes the user has created");
    }
}
