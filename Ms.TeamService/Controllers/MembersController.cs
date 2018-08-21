using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ms.TeamService.Models;
using Ms.TeamService.Persistence;

namespace Ms.TeamService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MembersController : Controller
    {
        private ITeamRepository _teamRepository;
        public MembersController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        [Route("api/teams/{teamId}/[controller]")]
        [HttpGet]
        public async Task<IActionResult> Get(Guid teamId)
        {
            var team = await _teamRepository.GetTeamById(teamId);
            if (team == null)
            {
                return NotFound(teamId);
            }

            return Ok(team.Members);
        }

        [Route("api/teams/{teamId}/[controller]")]
        [HttpPost]
        public async Task<IActionResult> Post(Guid teamId, [FromBody]Member member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var team = await _teamRepository.GetTeamById(teamId);
            if (team == null)
            {
                return NotFound(teamId);
            }

            var _member = new Member
            {
                FirstName = member.FirstName,
                LastName = member.LastName,
                MemberId = Guid.NewGuid()
            };

            await _teamRepository.AddTeamMember(team.TeamId, _member);

            return CreatedAtRoute("GetMembers", _member);
        }

        [Route("api/teams/{teamId}/[controller]/{memberId}")]
        [HttpPut]
        public async Task<IActionResult> Put(Guid teamId, Guid memberId, [FromBody]Member member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var team = await _teamRepository.GetTeamById(teamId);
            if (team == null)
            {
                return NotFound(teamId);
            }

            var _member = team.Members.Where(x => x.MemberId == memberId).FirstOrDefault();
            if (_member == null)
            {
                return NotFound(memberId);
            }

            _member.FirstName = member.FirstName;
            _member.LastName = member.LastName;

            await _teamRepository.UpdateTeamMember(team.TeamId, _member);

            return NoContent();
        }


        [Route("api/teams/{teamId}/[controller]/{memberId}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid teamId, Guid memberId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var team = await _teamRepository.GetTeamById(teamId);
            if (team == null)
            {
                return NotFound(teamId);
            }

            var member = team.Members.Where(x => x.MemberId == memberId).FirstOrDefault();
            if (member == null)
            {
                return NotFound(memberId);
            }

            await _teamRepository.DeleteTeamMember(team.TeamId, member.MemberId);

            return NoContent();

        }
    }
}
