using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    public class Email : BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; } 
    }
}
