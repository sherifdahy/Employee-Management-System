using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.DTOs
{
    public class CompanyDTO : BaseDTO   
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TaxRegistrationNumber { get; set; }
        public string TaxFileNumber { get; set; }
        public string EntityType { get; set; }
        public string TaxOfficeName { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        public List<OwnerDTO> Owners { get; set; } = new();
        public List<EmailDTO> Emails { get; set; } = new();
    }
}
