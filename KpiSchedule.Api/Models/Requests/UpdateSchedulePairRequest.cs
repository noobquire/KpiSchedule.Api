using KpiSchedule.Common.Entities;
using KpiSchedule.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace KpiSchedule.Api.Models.Requests
{
    public class UpdateSchedulePairRequest
    {
        /// <summary>
        /// Pair identifier.
        /// </summary>
        [Required]
        public Common.Entities.PairIdentifier PairId { get; set; }

        [EnumDataType(typeof(PairType))]
        [Required]
        /// <summary>
        /// Pair type <see cref="Common.Models.PairType"/>
        /// </summary>
        public string PairType { get; set; }

        /// <summary>
        /// Boolean indicating if pair occurs online.
        /// </summary>
        [Required]
        public bool IsOnline { get; set; }

        /// <summary>
        /// Pair subject.
        /// </summary>
        [Required]
        public SubjectEntity Subject { get; set; }

        /// <summary>
        /// Room(s) where pair occurs.
        /// </summary>
        public List<string> Rooms { get; set; }

        /// <summary>
        /// Link to join the online conference for this pair.
        /// </summary>
        public string OnlineConferenceUrl { get; set; }

        /// <summary>
        /// Name(s) of teacher(s) who conduct this pair.
        /// </summary>
        public List<string> TeacherNames { get; set; }
    }
}
