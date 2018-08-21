using System;

namespace Ms.TeamService.Models
{
    public class Member
    {
        public Guid MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public Guid? TeamId { get; set; }
        public Team Team { get; set; }
    }
}