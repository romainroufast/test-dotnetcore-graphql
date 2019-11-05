using AutoMapper;
using GraphQL;
using GraphQL.Relay.Types;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types.Relay;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            
            // singleton
            services.AddScoped<IDroidRepository, DroidRepository>();
            services.AddScoped<ISpaceshipRepository, SpaceshipRepository>();
            
            // graphql
            services.AddTransient(typeof(ConnectionType<>));
            services.AddTransient(typeof(EdgeType<>));
            services.AddTransient<NodeInterface>();
            services.AddTransient<PageInfoType>();
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<AppSchema>();
            services.AddGraphQL(o => { o.ExposeExceptions = false; }).AddGraphTypes(ServiceLifetime.Scoped);
            services.AddTransient<GetSpaceshipsQuery>();

            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddCors(o => o.AddPolicy("AllowAllPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
                
            // middleware, order is important
            app.UseCors("AllowAllPolicy");
            app.UseGraphQL<AppSchema>();
            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());
            app.UseMvc();
        }
    }
}