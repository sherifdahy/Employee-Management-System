using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.DTOs
{
    public class OrganizationDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public List<SelectorDTO> Selectors { get; set; } = new();
    }
}
