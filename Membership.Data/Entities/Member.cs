using System.Collections.Generic;

namespace Membership.Data.Entities
{
    public class Member
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int MembershipNumber { get; set; }
        public Club Club { get; set; }
        public int ClubId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
