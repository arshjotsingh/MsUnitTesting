using FluentAssertions;
using Ms.TeamService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
            var response = await _fixture.HttpClient.GetAsync("api/teams/ping");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Be("Pong");
        }

        [Fact]
        public async Task GetAllTeams_ReturnsTeams()
        {
            var teams = GetTestData();
            var response = await _fixture.HttpClient.GetAsync("api/teams");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var _teams = JsonConvert.DeserializeObject<IEnumerable<Team>>(responseString);
            _teams.Count().Should().Be(2);
        }

        [Fact]
        public async Task CreateTeam_AddsNewTeam()
        {
            var team = new Team
            {
                Name = "Team-3",
                TeamId = Guid.NewGuid()
            };

            var serializeTeam = JsonConvert.SerializeObject(team);
            var buffer = Encoding.UTF8.GetBytes(serializeTeam);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _fixture.HttpClient.PostAsync("api/teams", byteContent);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            Team t = JsonConvert.DeserializeObject<Team>(content);
            Assert.Equal(team.Name, t.Name);
        }


        private IEnumerable<Team> GetTestData()
        {
            var members1 = new Member[]
            {
                new Member
                {
                    FirstName = "Person-1",
                    LastName = "LastName-1",
                    MemberId = Guid.NewGuid()
                },

                new Member
                {
                    FirstName = "Person-2",
                    LastName = "LastName-2",
                    MemberId = Guid.NewGuid()
                },

                new Member
                {
                    FirstName = "Person-3",
                    LastName = "LastName-3",
                    MemberId = Guid.NewGuid()
                },
            };

            var members2 = new Member[]
            {
                 new Member
                {
                    FirstName = "Person-4",
                    LastName = "LastName-4",
                    MemberId = Guid.NewGuid()
                },

                new Member
                {
                    FirstName = "Person-5",
                    LastName = "LastName-5",
                    MemberId = Guid.NewGuid()
                },

                new Member
                {
                    FirstName = "Person-6",
                    LastName = "LastName-6",
                    MemberId = Guid.NewGuid()
                },
            };

            return new Team[]
            {
                new Team
                {
                    Name = "Team-1",
                    TeamId = Guid.NewGuid(),
                    Members = members1
                },
                new Team
                {
                    Name = "Team-2",
                    TeamId = Guid.NewGuid(),
                    Members = members2
                }
            };
        }
    }
}
