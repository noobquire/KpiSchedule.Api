﻿using System.Security.Cryptography;

namespace KpiSchedule.DataAccess.Entities
{
    /// <summary>
    /// Base DB entity for schedules.
    /// </summary>
    /// <typeparam name="TScheduleDay">Schedule day type.</typeparam>
    public abstract class BaseScheduleEntity<TScheduleDay, TSchedulePair> : BaseEntity where TScheduleDay : BaseScheduleDayEntity<TSchedulePair> where TSchedulePair : BaseSchedulePairEntity
    {
        /// <summary>
        /// Schedule unique identifier.
        /// </summary>
        public virtual Guid ScheduleId { get; set; }

        /// <summary>
        /// First week of the schedule.
        /// </summary>
        public List<TScheduleDay> FirstWeek { get; set; }

        /// <summary>
        /// Second week of the schedule.
        /// </summary>
        public List<TScheduleDay> SecondWeek { get; set; }

        private void CheckPairId(PairIdentifier pairId)
        {
            if (!new[] { 1, 2 }.Contains(pairId.WeekNumber))
            {
                throw new ArgumentException(nameof(pairId.WeekNumber), "Week number must be either 1 or 2");
            }

            var week = pairId.WeekNumber == 1 ? this.FirstWeek : this.SecondWeek;

            if (!Enumerable.Range(1, week.Count).Contains(pairId.DayNumber))
            {
                throw new ArgumentException(nameof(pairId.DayNumber), $"Day number must be between 1 and {week.Count}");
            }

            var day = week[pairId.DayNumber - 1];

            var pairNumbersThisDay = day.Pairs.Select(p => p.PairNumber).Distinct();
            if (!pairNumbersThisDay.Contains(pairId.PairNumber))
            {
                throw new ArgumentException(nameof(pairId.PairNumber), $"Pair number must be in [{string.Join(", ", pairNumbersThisDay)}]");
            }

            var pairs = day.Pairs.Where(p => p.PairNumber == pairId.PairNumber);
            if(pairs.Count() < pairId.DuplicatePairNumber || !pairs.Any() || pairId.DuplicatePairNumber < 1)
            {
                var minDuplicatePair = pairs.Any() ? 1 : 0;
                throw new ArgumentException(nameof(pairId), $"Duplicate pair number must be between {minDuplicatePair} and {pairs.Count()}");
            }
        }

        /// <summary>
        /// Get schedule pair by Id.
        /// </summary>
        /// <param name="pairId"></param>
        /// <returns></returns>
        public TSchedulePair GetPairById(PairIdentifier pairId)
        {
            CheckPairId(pairId);
            var week = pairId.WeekNumber == 1 ? this.FirstWeek : this.SecondWeek;
            var day = week.ElementAt(pairId.DayNumber - 1);
            var pairs = day.Pairs.Where(p => p.PairNumber == pairId.PairNumber);
            var dupPair = pairs.ElementAt(pairId.DuplicatePairNumber - 1);
            return dupPair;
        }

        /// <summary>
        /// Create new or update exisitng schedule pair.
        /// </summary>
        /// <param name="pairId">Pair identifier.</param>
        /// <param name="pairData">Pair data.</param>
        public void UpdateSchedulePair(PairIdentifier pairId, TSchedulePair pairData)
        {
            if (!new[] { 1, 2 }.Contains(pairId.WeekNumber))
            {
                throw new ArgumentException(nameof(pairId.WeekNumber), "Week number must be either 1 or 2");
            }

            var week = pairId.WeekNumber == 1 ? this.FirstWeek : this.SecondWeek;

            if (!Enumerable.Range(1, week.Count).Contains(pairId.DayNumber))
            {
                throw new ArgumentException(nameof(pairId.DayNumber), $"Day number must be between 1 and {week.Count}");
            }

            var day = week[pairId.DayNumber - 1];

            // Check only if 0 < pair number < 7 in case we want to create a new pair.
            if(pairId.PairNumber > 6 || pairId.PairNumber < 1)
            {
                throw new ArgumentException(nameof(pairId.PairNumber), $"Pair number must between 1 and 6");
            }

            var existingPairs = day.Pairs.Where(p => p.PairNumber == pairId.PairNumber);
            if(!existingPairs.Any())
            {
                // No pairs exist, creating a new one
                day.Pairs.Add(pairData);
                return;
            }

            if(pairId.DuplicatePairNumber < 1)
            {
                throw new ArgumentException(nameof(pairId.DuplicatePairNumber), $"Duplicate pair number must be > 0");
            }

            if (existingPairs.Count() >= pairId.DuplicatePairNumber)
            {
                // Update existing duplicate.
                var existingDuplicatePair = existingPairs.ElementAt(pairId.DuplicatePairNumber - 1);
                day.Pairs.Remove(existingDuplicatePair);
                day.Pairs.Add(pairData); // sorting of pairs must not rely on order in which they were added
            }

            // Create new duplicate.
            day.Pairs.Add(pairData);
        }

        /// <summary>
        /// Deletes a pair from the schedule.
        /// </summary>
        public void RemoveSchedulePair(PairIdentifier pairId)
        {
            CheckPairId(pairId);
            var week = pairId.WeekNumber == 1 ? this.FirstWeek : this.SecondWeek;
            var day = week.ElementAt(pairId.DayNumber - 1);
            var pairs = day.Pairs.Where(p => p.PairNumber == pairId.PairNumber);
            var dupPair = pairs.ElementAt(pairId.DuplicatePairNumber - 1);
            day.Pairs.Remove(dupPair);
        }
    }
}
