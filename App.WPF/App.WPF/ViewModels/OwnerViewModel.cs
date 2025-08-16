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
        string _phoneNumber;
        string _address;
        #endregion

        public Guid Id {  get; set; }
        [Required]
        public string Name { 
            get => _name; 
            set => SetProperty(ref _name,value); 
        }
        public string NationalId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
