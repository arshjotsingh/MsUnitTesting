using Ms.TeamService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ms.TeamService.Persistence
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetTeams();
        Task<Team> AddTeam(Team team);
        Task<Team> GetTeamById(Guid teamId);
        Task<Member> AddTeamMember(Member member);
        Task<Member> UpdateTeamMember(Member member);
        Task<Member> DeleteTeamMember(Guid memberId);
        Task<Team> UpdateTeam(Team team);
        Task<Team> DeleteTeam(Guid id);
        Task<IEnumerable<Member>> GetTeamMembersByTeamId(Guid teamId);
    }
}
