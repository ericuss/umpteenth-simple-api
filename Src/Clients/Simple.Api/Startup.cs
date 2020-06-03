// Copyright (c) simple. All rights reserved.

namespace Simple.Api
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
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
            services

                // .AddMediatR(Assembly.GetAssembly(typeof(GetDemosQuery)), Assembly.GetAssembly(typeof(CreateDemoCommand)), Assembly.GetAssembly(typeof(GetStateDemoQuery)))
                // .AddLogging(builder =>
                // {
                //     builder.AddApplicationInsights(this._configuration.GetValue<string>("ApplicationInsights:InstrumentationKey"));
                //     builder.AddFilter<ApplicationInsightsLoggerProvider>(string.Empty, LogLevel.Information);
                // })
                .AddApplicationInsightsTelemetry(x =>
                    x.InstrumentationKey =
                        this._configuration.GetValue<string>("ApplicationInsights:InstrumentationKey"))
                .AddCustomHealthChecks()
                .AddCustomSwagger()
                .AddCustomFixForHttps()
                .AddCustomHttps(this._configuration.GetValue<int>("https_port"), this._env)
                .AddHttpContextAccessor()
                .AddMvcCore()
                .AddApiExplorer()
                ;
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
    }
}