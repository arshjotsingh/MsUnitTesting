using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ms.TeamService.Tests.IntegrationTests
{
    public class TeamControllerTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;

        public TeamControllerTests(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task PingController()
        {
            var response = await _fixture.httpClient.GetAsync("api/teams");
            response.EnsureSuccessStatusCode();
            var responseStrong = await response.Content.ReadAsStringAsync();
            responseStrong.Should().Be("Pong");
        }
    }
}
