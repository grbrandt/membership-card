using System.Collections.Generic;

namespace Membership.Data.Entities
{
    /// <summary>
    /// Class Member encapsulates a person who has a membership in a <see cref="Club"/>.
    /// </summary>
    /// <seealso cref="Membership.Data.Entities.ISoftDeleteable" />
    public class Person : ISoftDeleteable
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name. This would typically not be an actual name, but
        /// a nickname, an unique identifier or similar.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }
        /// <summary>
        /// Gets the memberships.
        /// </summary>
        /// <value>The memberships.</value>
        public virtual ICollection<Membership> Memberships { get; } = new List<Membership>();
        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        public bool IsDeleted { get; set; }
    }
}
