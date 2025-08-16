using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.DTOs
{
    public class OwnerDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NationalId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public Guid CompanyId { get; set; }

    }

}
