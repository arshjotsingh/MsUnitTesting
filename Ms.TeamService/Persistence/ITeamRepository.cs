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
        Task<Team> GetTeamById(int teamId);
    }
}
