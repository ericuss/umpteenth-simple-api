// Copyright (c) simple. All rights reserved.

namespace Simple.IntegrationTests.Samples
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Simple.IntegrationTests.Core;
    using Xunit;

    public class SamplesWithFixtureTests : IClassFixture<WebHostFixture>
    {
        private readonly HttpClient _client;

        public SamplesWithFixtureTests(WebHostFixture exampleFixture)
        {
            this._client = exampleFixture.CreateClient();
        }

        [Fact]
        public async Task HealthCheckSampleTest()
        {
            var response = await this._client.GetAsync("/liveness");

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Healthy", responseString);
        }
    }
}
