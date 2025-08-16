using App.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    public class ApplicationUser : BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public UserType UserType { get; set; }
        public virtual ICollection<Company> Companies { get; set; } = new HashSet<Company>();
        public virtual Account Account { get; set; }
    }
}
