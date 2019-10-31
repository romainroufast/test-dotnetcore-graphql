using System;
using System.Threading.Tasks;

namespace Test.GraphQL.StarWars.Domain.Droid
{
    public interface IDroidRepository
    {
        Task<Guid> Add(Droid droid);

        Task<Droid> Get(Guid id);
    }
}