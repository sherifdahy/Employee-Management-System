using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    public class TransactionItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public virtual ICollection<DailyTransaction> DailyTransactions { get; set; } = new HashSet<DailyTransaction>();
    }
}
