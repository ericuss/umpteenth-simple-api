// Copyright (c) simple. All rights reserved.

namespace Simple.IntegrationTests.Core
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Hosting;
    using Simple.Api;
    using Xunit.Abstractions;

    public class WebHostFixture : WebApplicationFactory<Program>
    {
        // Must be set in each test
        public ITestOutputHelper Output { get; set; }

        protected override IHostBuilder CreateHostBuilder()
        {
            var hostBuilder = new HostBuilder()
               .ConfigureWebHost(webHost =>
               {
                   // Add TestServer
                   webHost.UseTestServer();
                   webHost.UseStartup<Startup>();
               });

            return hostBuilder;

            //// Build and start the IHost
            // var taskHost = hostBuilder.StartAsync();
            // taskHost.Wait();
            // return taskHost.Result;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Don't run IHostedServices when running as a test
            builder.ConfigureTestServices((services) =>
            {
                services.RemoveAll(typeof(IHostedService));
            });
        }
    }
}
