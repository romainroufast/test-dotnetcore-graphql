using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GraphiQl;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Test.GraphQL.StarWars.Domain.Droid;
using Test.GraphQL.StarWars.Domain.Spaceship;
using Test.GraphQL.StarWars.Infrastructure.GraphQL.Queries;
using Test.GraphQL.StarWars.Infrastructure.Repositories;

namespace Test.GraphQL
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder.AddConsole());
            services.AddAutoMapper(GetType());

            // db context
            services.AddDbContext<DroidRepository>(context => context.UseInMemoryDatabase("StarWarsDroids"));
            services.AddDbContext<SpaceshipRepository>(context => context.UseInMemoryDatabase("StarWarsSpaceships"));
            
            // singleton
            services.AddScoped<IDroidRepository, DroidRepository>();
            services.AddScoped<ISpaceshipRepository, SpaceshipRepository>();
            
            // graphql queries
            services.AddTransient<GetSpaceshipsQuery>();

            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
                
            // adding the GraphiQL UI
            app.UseGraphiQl("/graphiql", "/graph");
            
            app.UseMvc();
        }
    }
}