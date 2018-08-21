using Ms.TeamService.Models;
using Ms.TeamService.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ms.TeamService.Tests.IntegrationTests
{
    public class DatabaseSeeder
    {
        private readonly TeamDbContext _context;

        public DatabaseSeeder(TeamDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            var members1 = new Member[]
            {
                new Member
                {
                    FirstName = "Person-1",
                    LastName = "LastName-1",
                    MemberId = Guid.NewGuid()
                },

                new Member
                {
                    FirstName = "Person-2",
                    LastName = "LastName-2",
                    MemberId = Guid.NewGuid()
                },

                new Member
                {
                    FirstName = "Person-3",
                    LastName = "LastName-3",
                    MemberId = Guid.NewGuid()
                },
            };

            var members2 = new Member[]
            {
                 new Member
                {
                    FirstName = "Person-4",
                    LastName = "LastName-4",
                    MemberId = Guid.NewGuid()
                },

                new Member
                {
                    FirstName = "Person-5",
                    LastName = "LastName-5",
                    MemberId = Guid.NewGuid()
                },

                new Member
                {
                    FirstName = "Person-6",
                    LastName = "LastName-6",
                    MemberId = Guid.NewGuid()
                },
            };

            var teams = new Team[]
            {
                new Team
                {
                    Name = "Team-1",
                    TeamId = Guid.NewGuid(),
                    Members = members1
                },
                new Team
                {
                    Name = "Team-2",
                    TeamId = Guid.NewGuid(),
                    Members = members2
                }
            };

            await _context.Teams.AddRangeAsync(teams);
            await _context.Members.AddRangeAsync(members1);
            await _context.Members.AddRangeAsync(members2);
            await _context.SaveChangesAsync();
        }
    }
}
