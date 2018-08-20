using Ms.TeamService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ms.TeamService.Persistence
{
    public class MemoryTeamRepository : ITeamRepository
    {
        private static ICollection<Team> _teams;
        private TeamDbContext _context;
        public MemoryTeamRepository(TeamDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await Task.Run(() => _context.Teams.ToList());
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
            team.Members.Add(member);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeamMember(Guid teamId, Member member)
        {
            throw new NotImplementedException();
        }
        public Task DeleteTeamMember(Guid teamId, Guid memberId)
        {
            throw new NotImplementedException();
        }
    }
}
