// Copyright (c) simple. All rights reserved.

namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.OpenApi.Models;

    public static class SwaggerExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
                {
                    setup.DescribeAllParametersInCamelCase();
                    setup.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "simple Api",
                        Version = "v1",
                    });

                    setup.CustomSchemaIds(x => x.FullName);
                });

            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            return app.UseSwagger()
                      .UseSwaggerUI(setup =>
                      {
                          setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Innovation Demos");
                          setup.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                      });
        }
    }
}