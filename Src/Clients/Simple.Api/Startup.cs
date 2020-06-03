// Copyright (c) simple. All rights reserved.

namespace Simple.Api
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Simple.Data;
    using Simple.Infrastructure.Entities.Settings;

    public class Startup
    {
        private const string APPINSIGHTSINSTRUMENTATIONKEY = "ApplicationInsights:InstrumentationKey";
        private const string HTTPSPORT = "HTTPS_PORT";
        private const string DATABASE = "Database";
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _env;

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            this._configuration = configuration;
            this._env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var (database, httpsPort, applicationInsightsKey) = this.RegisterConfigurations(services);

            services

                // .AddMediatR(Assembly.GetAssembly(typeof(GetDemosQuery)), Assembly.GetAssembly(typeof(CreateDemoCommand)), Assembly.GetAssembly(typeof(GetStateDemoQuery)))
                // .AddLogging(builder =>
                // {
                //     builder.AddApplicationInsights(this._configuration.GetValue<string>("ApplicationInsights:InstrumentationKey"));
                //     builder.AddFilter<ApplicationInsightsLoggerProvider>(string.Empty, LogLevel.Information);
                // })
                .AddApplicationInsightsTelemetry(x =>
                    x.InstrumentationKey = applicationInsightsKey)
                .AddCustomHealthChecks()
                .AddCustomSwagger()
                .AddCustomFixForHttps()
                .AddCustomHttps(httpsPort, this._env)
                .AddHttpContextAccessor()
                .AddMvcCore()
                .AddApiExplorer()
                .Services
                .RegisterDataServices(database.LibraryConnectionString, database.ApplyMigrations, this._env)
                ; // string connectionString, bool migrate, IHostEnvironment env
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseCustomFixForHttps()
                .UseCustomTracing()
                .AddIf(env.IsDevelopment(), x => x.UseDeveloperExceptionPage())
                .UseFileServer()
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseRouting()
                .UseCustomSwagger()
                .UseEndpoints(endpoints =>
                {
                    endpoints.UseHealthChecks();
                    endpoints.MapControllers();
                })
                .UseWelcomePage()
               ;
        }

        private (Database, int, string) RegisterConfigurations(IServiceCollection services)
        {
            var httpsPort = this._configuration.GetValue<int>(HTTPSPORT);
            var connectionStrings = this._configuration.GetSection(DATABASE).Get<Database>();
            var instrumentationKey = this._configuration.GetValue<string>(APPINSIGHTSINSTRUMENTATIONKEY);

            services.Configure<Database>(this._configuration.GetSection(DATABASE));

            return (connectionStrings, httpsPort, instrumentationKey);
        }
    }
}