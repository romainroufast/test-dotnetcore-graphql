using GraphQL.Types;
using Test.GraphQL.Shared.ViewModels.StarWars;
using Test.GraphQL.StarWars.Domain.Droid;
using Test.GraphQL.StarWars.Domain.Spaceship;

namespace Test.GraphQL.StarWars.Infrastructure.GraphQL.Types
{
    public class SpaceshipType : ObjectGraphType<SpaceshipViewModel>
    {
        public SpaceshipType()
        {
            Name = "Spaceship";
            
            Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the Spaceship.");
            Field(x => x.Name).Description("The name of the Spaceship.");
            Field(x => x.Commander, type: typeof(DroidType)).Description("The commander of the Spaceship.");
        }
    }
}