using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpiSchedule.Services.Models
{
    public class JwtToken
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
