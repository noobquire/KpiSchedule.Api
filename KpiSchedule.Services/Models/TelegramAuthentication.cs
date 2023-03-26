namespace KpiSchedule.Services.Models
{
    public class TelegramAuthentication
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime AuthDate { get; set; }
        public string Hash { get; set; }
    }
}
