using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Test.GraphQL.StarWars.Domain.Spaceship;

namespace Test.GraphQL.StarWars.Infrastructure.Repositories
{
    public class SpaceshipRepository : DbContext, ISpaceshipRepository
    {
        public SpaceshipRepository(DbContextOptions<SpaceshipRepository> options) : base(options) {}
        
        public DbSet<Spaceship> Spaceships { get; set; }
        
        public async Task<Guid> Add(Spaceship spaceship)
        {
            var added = Spaceships.Add(spaceship);
            await SaveChangesAsync();
            Console.WriteLine($"Added Spaceship with id: {added.Entity.Id}");
            return added.Entity.Id;
        }

        public async Task AddRange(List<Spaceship> spaceships)
        {
            Spaceships.AddRange(spaceships);
            await SaveChangesAsync();
        }

        public Task<Spaceship> Get(Guid id)
        {
            return Task.FromResult(Spaceships.FirstOrDefault(t => t.Id == id));
        }

        public Task<List<Spaceship>> Get()
        {
            return Task.FromResult(Spaceships.ToList());
        }
    }
}