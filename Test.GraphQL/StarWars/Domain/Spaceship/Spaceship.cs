using System;

namespace Test.GraphQL.StarWars.Domain.Spaceship
{
    public class Spaceship
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public Guid CommanderId { get; set; }

        public Spaceship()
        {
            Id = Guid.NewGuid(); 
        }
    }
}