namespace KpiSchedule.DataAccess.Entities
{
    /// <summary>
    /// Base database entity.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Last updated UTC timestamp.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Created UTC timestamp.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Boolean indicating if the entity is deleted.
        /// Used for soft delete.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
