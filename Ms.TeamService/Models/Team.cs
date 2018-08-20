using System;
using System.Collections.Generic;

namespace Ms.TeamService.Models
{
    public class Team
    {
        public string Name { get; set; }
        public Guid TeamId { get; set; }
        public ICollection<Member> Members { get; set; }

        public Team()
        {
            this.Members = new List<Member>();
        }

        public Team(string name) : this()
        {
            Name = name;
        }

        public Team(string name, Guid teamId) : this(name)
        {
            TeamId = teamId;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
