using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ms.TeamService.Models;
using Ms.TeamService.Persistence;

namespace Ms.TeamService.Controllers
{
    public class TeamsController : Controller
    {
        private ITeamRepository _teamRepository;
        public TeamsController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        [Route("api/[controller]")]
        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            return Ok(await _teamRepository.GetTeams());
        }

        [Route("api/[controller]/{id}")]
        [HttpGet("{id}", Name = "GetTeam")]
        public async Task<IActionResult> GetTeamById(Guid teamId)
        {
            var team = await _teamRepository.GetTeamById(teamId);
            if (team == null)
            {
                return NotFound(teamId);
            }
            return Ok(await _teamRepository.GetTeamById(teamId));
        }

        [Route("api/[controller]")]
        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody]Team t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var team = new Team
            {
                TeamId = Guid.NewGuid(),
                Name = t.Name,
                AddedTime = DateTime.Now,
            };

            await _teamRepository.AddTeam(team);
            return CreatedAtRoute("GetTeam", new { id = team.TeamId }, team);
        }


        [Route("api/[controller]/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateTeam(Guid id, [FromBody]Team t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var team = await _teamRepository.GetTeamById(id);
            if (team == null)
            {
                return NotFound(id);
            }

            team.ModifiedTime = DateTime.Now;
            team.Name = t.Name;

            await _teamRepository.UpdateTeam(team);
            return NoContent();
        }

        [Route("api/[controller]/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var team = await _teamRepository.GetTeamById(id);
            if (team == null)
            {
                return NotFound(id);
            }
            await _teamRepository.DeleteTeam(id);
            return NoContent();
        }
    }
}
