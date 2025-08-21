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
        public string? Note { get; set; }
        public decimal Amount { get; set; }
        public string? FileUrl { get; set; }
        public int TransactionItemCategoryId { get; set; }
        public virtual TransactionItemCategory TransactionItemCategory { get; set; }
        public int DailyTransactionId { get; set; }
        public virtual DailyTransaction DailyTransaction { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
