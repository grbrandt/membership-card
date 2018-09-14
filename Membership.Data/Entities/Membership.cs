using System;
using System.Collections.Generic;
using System.Text;

namespace Membership.Data.Entities
{
    public class Membership
    {
        public int ClubId { get; set; }
        public Club Club { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public string MemberNumber { get; set; }

    }
}
