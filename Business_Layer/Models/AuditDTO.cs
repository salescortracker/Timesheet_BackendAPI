using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Models
{
    public class AuditDTO
    {
        public int AuditId { get; set; }            // PK
        public int UserId { get; set; }
        public string ActionType { get; set; } = string.Empty;
        public string ActionDetails { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;

        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
