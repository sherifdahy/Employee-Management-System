using App.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    public class Selector
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Value { get; set; }
        public SelectorType selectorType { get; set; }
        public ContentType contentType { get; set; }
    }

    
}
