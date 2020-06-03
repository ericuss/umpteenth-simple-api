// Copyright (c) simple. All rights reserved.

namespace Simple.IntegrationTests.Controllers.Products
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Simple.IntegrationTests.Core;
    using Xunit;
    using Xunit.Abstractions;

    public abstract class TestApiCore : IClassFixture<WebHostFixture>, IDisposable
    {
        public TestApiCore(WebHostFixture fixture, ITestOutputHelper output)
        {
            this.Fixture = fixture;
            fixture.Output = output;
            this.Client = fixture.CreateClient();

            // this.Context = fixture.Services.GetService(typeof(Context)) as Context;
            this.SeedData();

            // this.Context.SaveChanges();
        }

        protected WebHostFixture Fixture { get; }

        protected HttpClient Client { get; }

        // protected Context Context { get; }
        protected HttpResponseMessage Response { get; set; }

        public void Dispose() => this.Fixture.Output = null;

        protected virtual void SeedData()
        {
        }

        protected void GivenEmpty()
        {
        }

        protected void ResponseIsSuccess()
        {
            this.Response.EnsureSuccessStatusCode();
        }

        protected void ResponseIsFailed()
        {
            if (this.Response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
        }

        protected async Task<T> DeserializeResponse<T>()
        {
            var content = await this.Response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
