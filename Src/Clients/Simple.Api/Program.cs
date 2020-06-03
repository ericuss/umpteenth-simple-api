// Copyright (c) simple. All rights reserved.

namespace Simple.Api
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Simple.Data.SeedData;

    public class Program
    {
        public static void Main(string[] args)
        {
            var build = CreateHostBuilder(args).Build();

            var services = build.Services;
            SeedDataApplier.Apply(services).Wait();
            build.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((env, configBuilder) =>
                {
                    if (env.HostingEnvironment.IsDevelopment())
                    {
                        configBuilder.AddUserSecrets<Startup>(optional: true);
                    }

                    configBuilder.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseKestrel()
                        .UseSerilog((builderContext, config) => CreateSerilogLogger(builderContext, config))
                        .UseStartup<Startup>();
                });

        private static void CreateSerilogLogger(WebHostBuilderContext builderContext, LoggerConfiguration config)
        {
            var instrumentationKey = builderContext.Configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");

            // var configuration = ConfigureBuilder.GetConfiguration(builderContext.HostingEnvironment);
            // var instrumentationKey = configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");
            config
                .ReadFrom.Configuration(builderContext.Configuration)
                .WriteTo.ApplicationInsights(instrumentationKey, TelemetryConverter.Events, Serilog.Events.LogEventLevel.Information)
                .WriteTo.ApplicationInsights(instrumentationKey, TelemetryConverter.Traces, Serilog.Events.LogEventLevel.Information)
                ;
        }
    }
}
