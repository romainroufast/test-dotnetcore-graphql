using System.Threading.Tasks;
using GraphQL;
using GraphQL.Introspection;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Test.GraphQL.StarWars.Infrastructure.GraphQL.Queries;
using Test.GraphQL.StarWars.Infrastructure.GraphQL.Request;

namespace Test.GraphQL.StarWars.Infrastructure.GraphQL.Controllers
{
    [Route("graph")]
    [ApiController]
    public class SpaceshipController : Controller
    {
        private readonly GetSpaceshipsQuery _getSpaceshipsQuery;

        public SpaceshipController(GetSpaceshipsQuery getSpaceshipsQuery)
        {
            _getSpaceshipsQuery = getSpaceshipsQuery;
        }

        public async Task<IActionResult> Post([FromBody] GraphQlRequest query)
        {
            var inputs = query.Variables.ToInputs();

            var schema = new Schema
            {
                Query = _getSpaceshipsQuery
            };

            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
            });

            if(result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result.Data);
        }
    }
}