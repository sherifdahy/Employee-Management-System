using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.DTOs
{
    public class DataSyncDTO
    {
        public IEnumerable<ApplicationUserDTO> ApplicationUsers { get; set; } = new List<ApplicationUserDTO>();
        public IEnumerable<CompanyDTO> Companies { get; set; } = new List<CompanyDTO>();
        public IEnumerable<OrganizationDTO> Organizations { get; set; } = new List<OrganizationDTO>();
    }
}
