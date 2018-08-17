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

        public MemoryTeamRepository()
        {
        }

        public MemoryTeamRepository(ICollection<Team> teams)
        {
            _teams = teams;
        }

        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await Task.Run(() => _teams);
        }
        public async Task AddTeam(Team t)
        {
            await Task.Run(() => _teams.Add(t));
        }
        public async Task<Team> GetTeamById(int teamId)
        {
            return await Task.Run(() => _teams.Where(x => x.TeamId == teamId).FirstOrDefault());
        }
    }
}
