using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.ViewModels
{
    public class EmailViewModel : BaseViewModel
    {
        #region Private Property
        string _emailAddress;
        string _password;
        Guid _organizationId;
        #endregion

        public Guid Id { get; set; }
        [Required]
        public string EmailAddress {
            get => _emailAddress;
            set => SetProperty(ref _emailAddress, value);
        }
        [Required]
        public string Password { 
            get => _password; 
            set => SetProperty(ref _password,value); 
        }
        [Required]
        public Guid OrganizationId { 
            get => _organizationId; 
            set => SetProperty(ref _organizationId,value); 
        }
    }
}
