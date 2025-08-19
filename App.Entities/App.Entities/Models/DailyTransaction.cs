using App.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    public class DailyTransaction : BaseEntity
    {
        public int Id { get; set; }
        public TransactionState State { get; set; }
        public string? FileUrl { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public int ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<TransactionItem> TransactionItems { get; set; }
    }
}
