using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ms.TeamService.Dto;
using Ms.TeamService.Models;
using Ms.TeamService.Persistence;

namespace Ms.TeamService.Controllers
{
    [Produces("application/json")]
    public class MembersController : Controller
    {
        private ITeamRepository _teamRepository;
        public MembersController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        [HttpGet]
        [Route("api/teams/{id}/[controller]", Name = "GetMember")]
        public async Task<IActionResult> Get(Guid id)
        {
            var team = await _teamRepository.GetTeamById(id);
            if (team == null)
            {
                return NotFound(id);
            }

            var members = await _teamRepository.GetTeamMembersByTeamId(id);
            var _members = new List<MemberDto>();
            foreach (var _member in members)
            {
                _members.Add(new MemberDto
                {
                    MemberId = _member.MemberId,
                    AddedTime = _member.AddedTime,
                    FirstName = _member.FirstName,
                    LastName = _member.LastName,
                    ModifiedTime = _member.ModifiedTime,
                    TeamId = _member.TeamId
                });
            }
            return Ok(_members);
        }


        [Route("api/teams/{id}/[controller]")]
        [HttpPost]
        public async Task<IActionResult> Post(Guid id, [FromBody]Member member)
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

            var _member = new Member
            {
                FirstName = member.FirstName,
                LastName = member.LastName,
                MemberId = Guid.NewGuid(),
                AddedTime = DateTime.Now,
                Team = team
            };

            await _teamRepository.AddTeamMember(_member);

            return CreatedAtRoute("GetMember", new { id = _member.MemberId });
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
                return NotFound();
            }

            var members = await _teamRepository.GetTeamMembersByTeamId(teamId);
            if (members == null)
            {
                return NotFound();
            }

            var _member = members.Where(x => x.MemberId == memberId).FirstOrDefault();

            _member.FirstName = member.FirstName;
            _member.LastName = member.LastName;
            _member.ModifiedTime = DateTime.Now;
            _member.Team = team;
            _member.TeamId = team.TeamId;

            await _teamRepository.UpdateTeamMember(_member);

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
                return NotFound();
            }
            await _teamRepository.DeleteTeamMember(memberId);
            return NoContent();
        }
    }
}
