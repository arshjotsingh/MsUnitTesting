using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ms.TeamService.Models;
using Ms.TeamService.Persistence;

namespace Ms.TeamService.Controllers
{
    [Route("api/[controller]")]
    public class TeamsController : Controller
    {
        private ITeamRepository _teamRepository;
        public TeamsController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            return Ok(await _teamRepository.GetTeams());
        }

        [HttpGet]
        public async Task<IActionResult> GetTeamById(int teamId)
        {
            var team = await _teamRepository.GetTeamById(teamId);
            if (team == null)
            {
                return NotFound(teamId);
            }

            return Ok(await _teamRepository.GetTeamById(teamId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody]Team t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var team = await _teamRepository.GetTeamById(t.TeamId);
            if (team == null)
            {
                await _teamRepository.AddTeam(t);
                return Ok(t);
            }
            return BadRequest(ModelState);
        }
    }
}
