using Microsoft.EntityFrameworkCore;

namespace DigitalFamilyCookbook.GraphQL.Queries;

public class UserAccountQuery : ObjectGraphType<object>
{
    private readonly ApplicationDbContext _dbContext;

    public UserAccountQuery(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        Field<UserAccountType>(
            "UserAccountById",
            arguments: new QueryArguments(
                new QueryArgument<IntGraphType>
                {
                    Name = "id",
                    Description = "The ID of the user"
                }
            ),
            resolve: context =>
            {
                var id = context.GetArgument<string>("id");

                var user = _dbContext
                    .UserAccounts
                    .Include(u => u.RoleTypes)
                    .FirstOrDefault(u => u.UserId == id);

                return user;
            }
        );

        Field<UserAccountType>(
            "UserAccountByEmail",
            arguments: new QueryArguments(
                new QueryArgument<StringGraphType>
                {
                    Name = "email",
                    Description = "The email of the user"
                }
            ),
            resolve: context =>
            {
                var email = context.GetArgument<string>("email");

                var user = _dbContext
                    .UserAccounts
                    .Include(u => u.RoleTypes)
                    .FirstOrDefault(u => u.Email == email);

                return user;
            }
        );
    }
}
