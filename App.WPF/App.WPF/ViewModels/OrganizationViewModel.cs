using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.ViewModels
{
    public class OrganizationViewModel : BaseViewModel
    {
        #region Private Property
        string _name;
        string _url;
        #endregion


        public Guid Guid { get; set; }

        [Required]
        public string Name { 
            get => _name; 
            set => SetProperty(ref _name,value); 
        }
        [Required]
        public string URL { 
            get => _url; 
            set => SetProperty(ref _url,value); 
        }
        public ICollection<SelectorViewModel> Selectors { get; set; } = new ObservableCollection<SelectorViewModel>();
        public bool IsValid => ValidateAll();
    }
}
