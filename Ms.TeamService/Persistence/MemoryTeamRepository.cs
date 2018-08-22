using Microsoft.EntityFrameworkCore;
using Ms.TeamService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ms.TeamService.Persistence
{
    public class MemoryTeamRepository : ITeamRepository
    {
        private TeamDbContext _context;
        public MemoryTeamRepository(TeamDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _context.Teams.ToListAsync();
        }
        public async Task AddTeam(Team t)
        {
            _context.Teams.Add(t);
            await _context.SaveChangesAsync();
        }

        public async Task<Team> GetTeamById(Guid teamId)
        {
            return await _context.Teams.FindAsync(teamId);
        }

        public async Task AddTeamMember(Guid teamId, Member member)
        {
            var team = await _context.Teams.FindAsync(teamId);
            member.Team = team;
            member.TeamId = team.TeamId;
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeamMember(Guid teamId, Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTeamMember(Member member)
        {
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeam(Team team)
        {
            _context.Update(team);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeam(Guid id)
        {
            var team = await _context.Teams.FindAsync(id);
            _context.Remove(team);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Member>> GetTeamMembersByTeamId(Guid teamId)
        {
            return await _context.Members.Include(x => x.Team)
                .Where(x => x.TeamId == teamId).ToListAsync();
                
        }


        public async Task<Member> GetTeamMembersByMemberId(Guid memberId)
        {
            return await _context.Members.FindAsync(memberId);
        }
    }
}
