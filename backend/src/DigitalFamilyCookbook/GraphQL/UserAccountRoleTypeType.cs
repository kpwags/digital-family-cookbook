using DigitalFamilyCookbook.Data.Models;
using GraphQL.Types;

namespace DigitalFamilyCookbook.GraphQL
{
    public class UserAccountRoleTypeType : ObjectGraphType<UserAccountRoleType>
    {
        public UserAccountRoleTypeType()
        {
            Name = "RecipeMeat";

            Field(rm => rm.Id, type: typeof(IdGraphType)).Description("The ID of the UserAccountRoleTypeId");
            Field(rm => rm.UserAccountRoleTypeId, type: typeof(IntGraphType)).Description("The SQL ID of the UserAccountRoleTypeId");
            Field(rm => rm.UserAccountId, type: typeof(StringGraphType)).Description("The ID of the recipe");
            Field(rm => rm.UserAccount, type: typeof(UserAccountType)).Description("The user account");
            Field(rm => rm.RoleTypeId, type: typeof(IntGraphType)).Description("The ID of the role type");
            Field(rm => rm.RoleType, type: typeof(RoleTypeType)).Description("The role type");
        }
    }
}