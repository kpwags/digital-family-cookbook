using GraphQL.SystemTextJson;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFamilyCookbook.Controllers;

[Route("gql/user")]
[ApiController]
public class UserController : Controller
{
    private readonly ApplicationDbContext _db;

    public UserController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> LookupUser([FromBody] GraphQLQuery query)
    {
        var inputs = query.Variables.ToInputs();

        var schema = new Schema
        {
            Query = new UserAccountQuery(_db)
        };

        var result = await new DocumentExecuter().ExecuteAsync(_ =>
        {
            _.Schema = schema;
            _.Query = query.Query;
            _.OperationName = query.OperationName;
            _.Inputs = inputs;
        });

        if (result.Errors?.Count > 0)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}