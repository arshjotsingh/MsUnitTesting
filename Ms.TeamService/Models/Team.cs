using System;
using System.Collections.Generic;

namespace Ms.TeamService.Models
{
    public class Team
    {
        public string Name { get; set; }
        public int TeamId { get; set; }
        public ICollection<Member> Members { get; set; }

        public Team()
        {
            this.Members = new List<Member>();
        }

        public Team(string name) : this()
        {
            Name = name;
        }

        public Team(string name, int id) : this(name)
        {
            TeamId = id;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
