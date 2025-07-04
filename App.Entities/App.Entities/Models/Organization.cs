using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? URL { get; set; }
        public virtual ICollection<Email> Emails { get; set; } = new HashSet<Email>();
    }
}
