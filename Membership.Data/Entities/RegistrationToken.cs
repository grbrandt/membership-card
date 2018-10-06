using System;
using System.Collections.Generic;
using System.Text;

namespace Membership.Data.Entities
{
    /// <summary>
    /// RegistrationToken encapsulates a unique token that a member can use
    /// to register and activate their user profile and membership.
    /// </summary>
    public class RegistrationToken
    {
        public int MemberId { get; set; }
        public Person Member { get; set; }

        public int ClubId { get; set; }
        public Club Club { get; set; }

        public DateTime Expires { get; set; }
        public bool IsValid { get; set; }
        public string TokenValue { get; set; }
    }
}
