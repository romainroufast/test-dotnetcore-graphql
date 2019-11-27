using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GraphQL.Types;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Test.GraphQL.StarWars.Domain.Droid;
using Test.GraphQL.StarWars.Domain.Spaceship;

namespace Test.GraphQL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            
            // add one droid & spaceship
            var droidRepository = scope.ServiceProvider.GetService<IDroidRepository>();
            var spaceshipRepository = scope.ServiceProvider.GetService<ISpaceshipRepository>();
            var commanderId = await droidRepository.Add(new Droid
            {
                Name = "R2-D2"
            });
            await spaceshipRepository.Add(new Spaceship
            {
                Name = "Falcon",
                CommanderId = commanderId
            });
            await spaceshipRepository.Add(new Spaceship
            {
                Name = "Commander II",
                CommanderId = commanderId
            });

            host.Run();
        }

        public static async Task DoQuery()
        {
            Console.WriteLine("DOQUERY");
            var postUri = "https://localhost:5001/graphql";
            var httpClient = new HttpClient();
            var query = @"
                {
                  spaceships {
                    name
                  }
                }";

            var postData = new  { Query = query };
            var stringContent = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
            
            var res = await httpClient.PostAsync(postUri, stringContent);
            if (res.IsSuccessStatusCode)
            {
                var content = await res.Content.ReadAsStringAsync();

                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine($"Error occurred... Status code:{res.StatusCode}");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}