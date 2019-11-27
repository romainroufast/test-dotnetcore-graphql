using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.GraphQL.StarWars.Domain.Spaceship;

namespace Test.GraphQL.StarWars.Infrastructure.Repositories
{
    public class SpaceshipRepository : ISpaceshipRepository
    {
        private List<Spaceship> Spaceships { get; set; }

        public SpaceshipRepository()
        {
            Spaceships = new List<Spaceship>();
        }
        
        public async Task<Guid> Add(Spaceship spaceship)
        {
            Spaceships.Add(spaceship);
            Console.WriteLine($"Added Spaceship with id: {spaceship.Id.ToString()}");
            return spaceship.Id;
        }

        public async Task AddRange(List<Spaceship> spaceships)
        {
            Spaceships.AddRange(spaceships);
        }

        public Task<Spaceship> Get(Guid id)
        {
            return Task.FromResult(Spaceships.FirstOrDefault(t => t.Id == id));
        }

        public Task<List<Spaceship>> Get()
        {
            return Task.FromResult(Spaceships);
        }
    }
}