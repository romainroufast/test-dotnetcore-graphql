using System;
using System.Collections.Generic;

namespace Test.GraphQL.Shared.ViewModels.StarWars
{
    public class DroidViewModel
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public List<SpaceshipViewModel> Spaceships { get; set; }
    }
}