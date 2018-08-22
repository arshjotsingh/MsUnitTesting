using Microsoft.EntityFrameworkCore;
using Ms.TeamService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ms.TeamService.Persistence
{
    public class TeamRepository : ITeamRepository
    {
        private TeamDbContext _context;
        public TeamRepository(TeamDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _context.Teams.ToListAsync();
        }
        public async Task<Team> AddTeam(Team t)
        {
            _context.Teams.Add(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public async Task<Team> GetTeamById(Guid teamId)
        {
            return await _context.Teams.FindAsync(teamId);
        }

        public async Task<Member> AddTeamMember(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task<Member> UpdateTeamMember(Member member)
        {
            _context.Entry(member).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return member;
        }
        public async Task<Member> DeleteTeamMember(Guid memberId)
        {
            var member = await _context.Members.FindAsync(memberId);
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task<Team> UpdateTeam(Team team)
        {
            _context.Entry(team).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return team;
        }

        public async Task<Team> DeleteTeam(Guid id)
        {
            var team = await _context.Teams.FindAsync(id);
            _context.Remove(team);
            await _context.SaveChangesAsync();
            return team;
        }

        public async Task<IEnumerable<Member>> GetTeamMembersByTeamId(Guid teamId)
        {
            return await _context.Members.Include(x => x.Team)
                .Where(x => x.TeamId == teamId).ToListAsync();
        }
    }
}
