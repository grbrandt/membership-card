using System;
using System.Collections.Generic;
using System.Text;

namespace Membership.Data.Entities
{
    public class ClubMember
    {
        public int ClubId { get; set; }
        public Club Club { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }
    }
}
