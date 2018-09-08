namespace Membership.Data.Entities
{
    /// <summary>
    /// Interface ISoftDeleteable
    /// </summary>
    interface ISoftDeleteable
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        bool IsDeleted { get; set; }
    }
}
