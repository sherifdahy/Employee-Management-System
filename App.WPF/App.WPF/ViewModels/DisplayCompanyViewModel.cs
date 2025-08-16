using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.ViewModels
{
    public class DisplayCompanyViewModel
    {
        public string Name { get; set; }
        public string TaxRegistrationNumber { get; set; }
        public string TaxFileNumber { get; set; }
        public string EntityType { get; set; }
        public string TaxOfficeName { get; set; }
        public string Address { get; set; }
        public ICollection<OwnerViewModel> Owners { get; set; }
        public IDictionary<string, ICollection<EmailViewModel>> Emails { get; set; } = new Dictionary<string, ICollection<EmailViewModel>>();
    }
}
