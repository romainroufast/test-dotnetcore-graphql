using AutoMapper;
using Test.GraphQL.Shared.ViewModels.StarWars;
using Test.GraphQL.StarWars.Domain.Droid;
using Test.GraphQL.StarWars.Domain.Spaceship;

namespace Test.GraphQL.StarWars.Infrastructure.GraphQL.Profiles
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            CreateMap<DroidViewModel, Droid>().ReverseMap();
            CreateMap<SpaceshipViewModel, Spaceship>().ReverseMap();
        }
    }
}