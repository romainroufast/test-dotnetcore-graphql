using GraphQL;
using GraphQL.Types;
using Test.GraphQL.StarWars.Infrastructure.GraphQL.Queries;

namespace Test.GraphQL.StarWars.Infrastructure.GraphQL
{
    public class AppSchema : Schema
    {
        public AppSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<GetSpaceshipsQuery>();
        }
    }
}