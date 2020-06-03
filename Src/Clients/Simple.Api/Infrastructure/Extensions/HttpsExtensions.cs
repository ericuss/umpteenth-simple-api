// Copyright (c) simple. All rights reserved.

namespace Microsoft.Extensions.DependencyInjection
{
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Hosting;

    public static class HttpsExtensions
    {
        public static IServiceCollection AddCustomHttps(this IServiceCollection services, int httpsPort, IHostEnvironment env)
        {
            services
                .AddIf(!env.IsDevelopment(), x => x.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                    options.HttpsPort = httpsPort;
                }))
                ;
            return services;
        }

        public static IApplicationBuilder UseCustomHttps(this IApplicationBuilder app, IHostEnvironment env)
        {
            return app
                    .AddIf(!env.IsDevelopment(), x => x.UseHsts())
                    .AddIf(!env.IsDevelopment(), x => x.UseHttpsRedirection())
                ;
        }
    }
}