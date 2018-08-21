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
        [HttpGet]
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

            await _teamRepository.AddTeam(t);
            return CreatedAtRoute("GetTeams", t);
        }

        [HttpGet]
        [Route("api/[controller]/ping")]
        public string Get()
        {
            return "Pong";
        }

    }
}
