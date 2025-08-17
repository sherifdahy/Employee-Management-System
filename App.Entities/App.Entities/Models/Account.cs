using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    public class Account 
    {
        [Key]
        [ForeignKey(nameof(User))]
        public int Id { get; set; }
        public decimal Currency { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
