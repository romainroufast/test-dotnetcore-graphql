using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Test.GraphQL.StarWars.Domain.Droid;

namespace Test.GraphQL.StarWars.Infrastructure.Repositories
{
    public class DroidRepository : DbContext, IDroidRepository
    {
        public DroidRepository(DbContextOptions<DroidRepository> options) : base(options) {}
        
        public DbSet<Droid> Droids { get; set; }
        
        public async Task<Guid> Add(Droid droid)
        {
            var added = Droids.Add(droid);
            await SaveChangesAsync();
            Console.WriteLine($"Added Droid with id: {added.Entity.Id}");
            return added.Entity.Id;
        }

        public Task<Droid> Get(Guid id)
        {
            return Task.FromResult(Droids.FirstOrDefault(t => t.Id == id));
        }
    }
}