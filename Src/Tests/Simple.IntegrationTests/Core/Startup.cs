// Copyright (c) simple. All rights reserved.

namespace Simple.IntegrationTests.Core
{
    using System.IO;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Simple.Api.Controllers;

    public class Startup
    {
        // private const string CONNECTION_STRING = "ConnectionStrings";
        private readonly IHostEnvironment _env;

        public Startup(IHostEnvironment env)
        {
            var authenticationTestsPath = Directory.GetCurrentDirectory();
            var appJsonPath = Path.GetFullPath(Path.Combine(authenticationTestsPath, "appsettings.tests.json"));
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(appJsonPath, optional: false)
                .AddEnvironmentVariables();

            this.Configuration = configBuilder.Build();
            this._env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // var connectionString = this.RegisterConfigurations(services);
            services

                // .AddMediatR(Assembly.GetAssembly(typeof(GetDemosQuery)))
                .AddHttpContextAccessor()
                .AddMvc()
                    .AddApplicationPart(typeof(HomeController).Assembly)
                .Services
                .AddCustomHealthChecks()

                // .RegisterDataServices(connectionString.InnovationDemos, true, this._env)
                ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.UseHealthChecks();
                    endpoints.MapControllers();
                });
        }

        // private ConnectionStrings RegisterConfigurations(IServiceCollection services)
        // {
        //    var connectionStrings = this.Configuration.GetSection(CONNECTION_STRING).Get<ConnectionStrings>();
        //    services.Configure<ConnectionStrings>(this.Configuration.GetSection(CONNECTION_STRING));
        //    return connectionStrings;
        // }
    }
}
