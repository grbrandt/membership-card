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
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Membership> Memberships { get; } = new List<Membership>();
        public bool IsDeleted { get; set; }
    }
}
