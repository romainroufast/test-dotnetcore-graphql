using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test.GraphQL.StarWars.Domain.Spaceship
{
    public interface ISpaceshipRepository
    {
        Task<Guid> Add(Spaceship spaceship);
        
        Task AddRange(List<Spaceship> spaceships);

        Task<Spaceship> Get(Guid id);
        
        Task<List<Spaceship>> Get();
    }
}