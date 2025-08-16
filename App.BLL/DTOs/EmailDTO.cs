using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.DTOs
{
    public class EmailDTO 
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Guid CompanyId { get; set; }
        public Guid OrganizationId { get; set; }
    }
}
