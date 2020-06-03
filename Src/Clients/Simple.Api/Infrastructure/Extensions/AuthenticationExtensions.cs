// Copyright (c) simple. All rights reserved.

namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;

    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, string authority, string audience)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.Audience = audience;
            });

            return services;
        }
    }
}