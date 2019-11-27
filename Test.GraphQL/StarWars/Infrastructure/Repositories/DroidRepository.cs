using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.GraphQL.StarWars.Domain.Droid;

namespace Test.GraphQL.StarWars.Infrastructure.Repositories
{
    public class DroidRepository : IDroidRepository
    {
        private List<Droid> Droids { get; set; }

        public DroidRepository()
        {
            Droids = new List<Droid>();
        }
        
        public async Task<Guid> Add(Droid droid)
        {
            Droids.Add(droid);
            Console.WriteLine($"Added Droid with id: {droid.Id.ToString()}");
            return droid.Id;
        }

        public Task<Droid> Get(Guid id)
        {
            return Task.FromResult(Droids.FirstOrDefault(t => t.Id == id));
        }
    }
}