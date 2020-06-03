// Copyright (c) simple. All rights reserved.

namespace Microsoft.AspNetCore.Builder
{
    public static class TracingExtensions
    {
        public static IApplicationBuilder UseCustomTracing(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TracingMiddleware>();
        }
    }
}