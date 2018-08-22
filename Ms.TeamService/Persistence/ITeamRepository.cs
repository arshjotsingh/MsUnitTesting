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
        Task AddTeam(Team team);
        Task<Team> GetTeamById(Guid teamId);
        Task AddTeamMember(Guid teamId, Member member);
        Task UpdateTeamMember(Guid teamId, Member member);
        Task DeleteTeamMember(Member member);
        Task UpdateTeam(Team team);
        Task DeleteTeam(Guid id);
        Task<IEnumerable<Member>> GetTeamMembersByTeamId(Guid teamId);
        Task<Member> GetTeamMembersByMemberId(Guid memberId);
    }
}
