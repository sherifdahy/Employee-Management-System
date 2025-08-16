using App.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.DTOs
{
    public class SelectorDTO 
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public SelectorType SelectorType { get; set; }
        public ContentType ContentType { get; set; }
    }
}
