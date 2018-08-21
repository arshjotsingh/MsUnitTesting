using System;
using System.Collections.Generic;

namespace Ms.TeamService.Models
{
    public class Team
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public ICollection<Member> Members { get; set; }
    }
}
