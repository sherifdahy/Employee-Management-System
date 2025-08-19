using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.ViewModels
{
    public class OwnerViewModel : BaseViewModel
    {
        #region Private Property
        string _name;
        string _nationalId;
        string _phoneNumber;
        string _address;
        #endregion

        public int Id {  get; set; }
        [Required]
        public string Name { 
            get => _name; 
            set => SetProperty(ref _name,value); 
        }
        [Required]
        public string NationalId {
            get => _nationalId;
            set => SetProperty(ref _nationalId, value);
        }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
