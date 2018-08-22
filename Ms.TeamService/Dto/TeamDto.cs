using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ms.TeamService.Dto
{
    public class TeamDto
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
    }
}
