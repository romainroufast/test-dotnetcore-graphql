using AutoMapper;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Test.GraphQL.StarWars.Domain.Droid;
using Test.GraphQL.StarWars.Domain.Spaceship;
using Test.GraphQL.StarWars.Infrastructure.GraphQL;
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
            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddLogging(builder => builder.AddConsole());
            services.AddAutoMapper(GetType());

            // db context
            services.AddDbContext<DroidRepository>(context => context.UseInMemoryDatabase("StarWarsDroids"));
            services.AddDbContext<SpaceshipRepository>(context => context.UseInMemoryDatabase("StarWarsSpaceships"));
            
            // singleton
            services.AddScoped<IDroidRepository, DroidRepository>();
            services.AddScoped<ISpaceshipRepository, SpaceshipRepository>();
            
            // graphql
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<AppSchema>();
            services.AddGraphQL(o => { o.ExposeExceptions = false; }).AddGraphTypes(ServiceLifetime.Scoped);
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
//            else
//            {
//                app.UseHsts();
//            }
                
            // graphql
            app.UseGraphQL<AppSchema>();
            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());
            
            app.UseMvc();
        }
    }
}