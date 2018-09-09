using System.Collections.Generic;

namespace Membership.Data.Entities
{
    /// <summary>
    /// The Club class represents a club and holds a reference to all <see cref="Member"/>s
    /// that belongs to it.
    /// </summary>
    /// <seealso cref="Membership.Data.Entities.ISoftDeleteable" />
    public class Club : ISoftDeleteable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public ClubSettings Settings { get; set; }
        public bool IsDeleted { get; set; }
    }
}