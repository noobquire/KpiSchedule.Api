namespace KpiSchedule.DataAccess
{
    /// <summary>
    /// Pair identifier.
    /// </summary>
    public class PairIdentifier
    {
        /// <summary>
        /// Week number, 1 or 2
        /// </summary>
        public int WeekNumber { get; set; }

        /// <summary>
        /// 1-based day number.
        /// </summary>
        public int DayNumber { get; set; }

        /// <summary>
        /// 1-based pair number.
        /// </summary>
        public int PairNumber { get; set; }
    }
}