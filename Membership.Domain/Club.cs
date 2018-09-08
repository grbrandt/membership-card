using System.Collections;
using System.Collections.Generic;

namespace Membership.Domain
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Member> Members { get; set; }

    }
}