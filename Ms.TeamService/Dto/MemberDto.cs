using System;

namespace Ms.TeamService.Dto
{
    public class MemberDto
    {
        public Guid MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public Guid? TeamId { get; set; }
    }
}
