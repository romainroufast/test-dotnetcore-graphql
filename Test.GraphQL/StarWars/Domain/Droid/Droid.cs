using System;
using System.Collections.Generic;

namespace Test.GraphQL.StarWars.Domain.Droid
{
    public class Droid
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public Droid()
        {
            Id = Guid.NewGuid();
        }
    }
    
    
}