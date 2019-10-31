using Newtonsoft.Json.Linq;

namespace Test.GraphQL.StarWars.Infrastructure.GraphQL.Request
{
    public class GraphQlRequest
    {
        public string OperationName { get; set; }
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }
}