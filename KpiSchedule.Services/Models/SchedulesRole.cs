using Amazon.DynamoDBv2.DataModel;

namespace KpiSchedule.Services.Models
{
    [DynamoDBTable("KpiSchedule-UserRoles")]
    public class ApplicationRole
    {
        [DynamoDBHashKey]
        [DynamoDBProperty("RoleId")]
        public int RoleId { get; set; }

        [DynamoDBProperty("RoleName")]
        public string RoleName { get; set; }

        [DynamoDBProperty("NormalizedRoleName")]
        public string NormalizedRoleName { get; set; }
    }
}
