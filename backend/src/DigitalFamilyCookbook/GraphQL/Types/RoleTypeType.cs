namespace DigitalFamilyCookbook.GraphQL.Types;

public class RoleTypeType : ObjectGraphType<RoleType>
{
    public RoleTypeType()
    {
        Name = "RoleType";

        Field(rt => rt.Id, type: typeof(IdGraphType)).Description("The ID of the role type");
        Field(rt => rt.RoleTypeId, type: typeof(IntGraphType)).Description("The SQL ID of the role type");
        Field(rt => rt.Name, type: typeof(StringGraphType)).Description("The name of the role type");
        Field(rt => rt.UserAccountRoleTypes, type: typeof(ListGraphType<UserAccountRoleTypeType>)).Description("The collection of UserAccountRoleTypes");
    }
}
