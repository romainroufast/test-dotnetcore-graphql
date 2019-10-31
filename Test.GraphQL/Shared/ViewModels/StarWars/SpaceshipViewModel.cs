using System;

namespace Test.GraphQL.Shared.ViewModels.StarWars
{
    public class SpaceshipViewModel
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public Guid CommanderId { get; set; }
        
        public DroidViewModel Commander { get; set; }
        
    }
}