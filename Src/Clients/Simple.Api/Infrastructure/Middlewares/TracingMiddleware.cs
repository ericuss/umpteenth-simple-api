// Copyright (c) simple. All rights reserved.

namespace Microsoft.AspNetCore.Builder
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using StackExchange.Profiling;

    public class TracingMiddleware
    {
        private const string MiniprofilerKey = "TraceRequests";
        private const string HeaderName = "X-Response-Time";
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public TracingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this._next = next;
            this._logger = loggerFactory.CreateLogger<TracingMiddleware>();
        }

        public async Task Invoke(HttpContext context, IConfiguration configuration)
        {
            var canTraceRequest = configuration.GetValue<bool?>(MiniprofilerKey);

            if (canTraceRequest.HasValue && canTraceRequest.Value)
            {
                await this.TraceRequests(context);
            }
            else
            {
                await this._next(context);
            }
        }

        private async Task TraceRequests(HttpContext context)
        {
            var profiler = MiniProfiler.StartNew("Tracing");
            using (profiler.Step("Request"))
            {
                context.Response.OnStarting(
                    state =>
                    {
                        var httpContext = (HttpContext)state;
                        profiler.Stop();
                        var profilerText = profiler.RenderPlainText().Replace("\n", " ").Replace("\r", " ").ToString();

                        httpContext.Response.Headers.Add(HeaderName, new[] { profilerText });
                        return Task.FromResult(0);
                    }, context);

                await this._next(context);
            }

            this._logger.LogInformation(
                "Request {method} {url} => {statusCode} \n{profiler}",
                context.Request?.Method,
                context.Request?.Path.Value,
                context.Response?.StatusCode,
                profiler.RenderPlainText());
        }
    }
}