using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.DTOs
{
    public class DataSyncDTO
    {
        public List<ApplicationUserDTO> ApplicationUsers { get; set; } = new();
        public List<CompanyDTO> Companies { get; set; } = new ();
        public List<OrganizationDTO> Organizations { get; set; } = new ();
    }
}
