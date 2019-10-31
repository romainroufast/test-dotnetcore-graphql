using GraphQL.Types;
using Test.GraphQL.Shared.ViewModels.StarWars;

namespace Test.GraphQL.StarWars.Infrastructure.GraphQL.Types
{
    public class DroidType : ObjectGraphType<DroidViewModel>
    {
        public DroidType()
        {
            Name = "Droid";
            
            Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the Droid.");
            Field(x => x.Name).Description("The name of the Droid.");
            Field(x => x.Spaceships, type: typeof(ListGraphType<SpaceshipType>)).Description("All of the commander's spaceships.");
        }
    }
}