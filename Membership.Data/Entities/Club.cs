using System.Collections.Generic;

namespace Membership.Data.Entities
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public bool IsDeleted { get; set; }
    }
}