using Microsoft.AspNetCore.Mvc;
using Moq;
using Ms.TeamService.Controllers;
using Ms.TeamService.Models;
using Ms.TeamService.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ms.TeamService.Tests
{
    public class TeamsControllerTests
    {
        [Fact]
        public async Task CreateTeam_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange and Act
            var mockRepo = new Mock<ITeamRepository>();
            var controller = new TeamsController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "errormessage");

            // Act
            var result = await controller.CreateTeam(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetTeamById_ReturnsHttpNotFound_ForInvalidTeam()
        {
            // Arrange
            Guid teamId = Guid.NewGuid();
            var mockRepo = new Mock<ITeamRepository>();
            mockRepo.Setup(x => x.GetTeamById(teamId)).Returns(Task.FromResult((Team)null));
            var controller = new TeamsController(mockRepo.Object);

            // Act
            var result = await controller.GetTeamById(teamId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetAllTeams_ReturnsOk()
        {
            //Arrange
            Mock<ITeamRepository> mockRepo = new Mock<ITeamRepository>();
            mockRepo.Setup(x => x.GetTeams()).Returns(Task.FromResult(GetTestTeams())).Verifiable();
            TeamsController controller = new TeamsController(mockRepo.Object);

            // Act
            var result = await controller.GetAllTeams();

            //Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Team>>(viewResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async void CreateTeam_ReturnsOk_ForValidModel()
        {
            //Arrange
            Mock<ITeamRepository> mockRepo = new Mock<ITeamRepository>();
            TeamsController controller = new TeamsController(mockRepo.Object);
            var team = GetTestTeamCreate();
            mockRepo.Setup(x => x.AddTeam(team)).Returns(Task.CompletedTask);

            //Act
            var result = await controller.CreateTeam(team);

            //Assert
            var okResult = Assert.IsType<CreatedAtRouteResult>(result);
            var returnTeam = Assert.IsType<Team>(okResult.Value);

            Assert.Equal("Ont", returnTeam.Name);
            Assert.Single(returnTeam.Members);
        }


        private IEnumerable<Team> GetTestTeams()
        {
            return new Team[]
            {
                new Team{Name="one"},
                new Team{Name="one"},
            };
        }

        private Team GetTestTeamCreate()
        {
            return new Team
            {
                Name = "Ont",
                Members = new Member[]
                {
                    new Member
                    {
                        FirstName = "Arsh",
                        LastName = "Hunjan"
                    }
                }
            };
        }
    }
}
