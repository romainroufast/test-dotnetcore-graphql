using System;
using System.Collections.Generic;
using AutoMapper;
using GraphQL.Types;
using Test.GraphQL.Shared.ViewModels.StarWars;
using Test.GraphQL.StarWars.Domain.Droid;
using Test.GraphQL.StarWars.Domain.Spaceship;
using Test.GraphQL.StarWars.Infrastructure.GraphQL.Types;

namespace Test.GraphQL.StarWars.Infrastructure.GraphQL.Queries
{
    public class GetSpaceshipsQuery : ObjectGraphType
    {
        public GetSpaceshipsQuery(IDroidRepository droidRepository, ISpaceshipRepository spaceshipRepository, IMapper mapper)
        {
            // one spaceship
            Field<SpaceshipType>(
                "Spaceship",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> {Name = "id", Description = "The id of the spaceship"}),
                resolve: context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var spaceship = mapper.Map<SpaceshipViewModel>(spaceshipRepository.Get(id).Result);
                    
                    if (spaceship == null) throw new ArgumentException("Wrong id for spaceship.");

                    spaceship.Commander = mapper.Map<DroidViewModel>(droidRepository.Get(spaceship.CommanderId).Result);

                    return spaceship;
                }
            );
            
            // list of spaceships
            Field<ListGraphType<SpaceshipType>>(
                "Spaceships",
                resolve: context =>
                {
                    var spaceships = mapper.Map<List<SpaceshipViewModel>>(spaceshipRepository.Get().Result);

                    foreach (var spaceship in spaceships)
                    {
                        spaceship.Commander = mapper.Map<DroidViewModel>(droidRepository.Get(spaceship.CommanderId).Result);
                    }

                    return spaceships;
                });

        }
        
        
    }
}