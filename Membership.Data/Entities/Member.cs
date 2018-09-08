namespace Membership.Data.Entities
{
    /// <summary>
    /// Class Member encapsulates a person who has a membership in a <see cref="Club"/>.
    /// </summary>
    /// <seealso cref="Membership.Data.Entities.ISoftDeleteable" />
    public class Member : ISoftDeleteable
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
