// Copyright (c) simple. All rights reserved.

namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.HttpOverrides;

    public static class HttpsRedirectionFixExtensions
    {
        public static IServiceCollection AddCustomFixForHttps(this IServiceCollection services)
        {
            // Configure reverse proxy https://github.com/IdentityServer/IdentityServer4/issues/1331
            services.Configure<ForwardedHeadersOptions>(options =>
                {
                    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

                    // ref: https://github.com/aspnet/Docs/issues/2384
                    options.RequireHeaderSymmetry = false;
                    options.KnownNetworks.Clear();
                    options.KnownProxies.Clear();
                });

            return services;
        }

        public static IApplicationBuilder UseCustomFixForHttps(this IApplicationBuilder app)
        {
            return app.UseForwardedHeaders();
        }
    }
}