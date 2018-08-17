using System;

namespace Ms.TeamService.Models
{
    public class Member
    {
        public int MemeberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Member()
        {
        }
        public Member(int id) : this()
        {
            MemeberId = id;
        }
        public Member(string firstName,
        string lastName, int id) : this(id)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public override string ToString()
        {
            return LastName;
        }
    }
}