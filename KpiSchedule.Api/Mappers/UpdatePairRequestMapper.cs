using KpiSchedule.Api.Models.Requests;
using KpiSchedule.Common.Entities;
using KpiSchedule.Common.Models;
using KpiSchedule.Common.Parsers;
using KpiSchedule.Common.Utils;

namespace KpiSchedule.Api.Mappers
{
    public static class UpdatePairRequestMapper
    {
        public static StudentSchedulePairEntity MapToEntity(this UpdateSchedulePairRequest request)
        {
            var pairType = (PairType) Enum.Parse(typeof(PairType), request.PairType);
            var (pairStart, pairEnd) = PairSchedule.GetPairStartAndEnd(request.PairId.PairNumber);
            var entity = new StudentSchedulePairEntity
            {
                Teachers = request.TeacherNames?.Select(t => new TeacherEntity { TeacherName = t, TeacherFullName = t }).ToList(),
                OnlineConferenceUrl = request.OnlineConferenceUrl,
                PairType = pairType.ToEnumString(),
                PairNumber = request.PairId.PairNumber,
                StartTime = pairStart.ToString("t"),
                EndTime = pairEnd.ToString("t"),
                IsOnline = request.IsOnline,
                Subject = request.Subject,
                Rooms = request.Rooms
            };
            return entity;
        }
    }
}
