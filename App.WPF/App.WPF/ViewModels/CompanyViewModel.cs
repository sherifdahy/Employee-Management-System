using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.ViewModels
{
    public class CompanyViewModel : BaseViewModel
    {
        #region Private Property
        string _name;
        string _taxRegistrationNumber;
        string _taxFileNumber;
        string _entityType;
        string _taxOfficeName;
        string _address;
        #endregion

        public Guid Id { get; set; }
        [Required(ErrorMessage = "اسم الشركة مطلوب")]
        public string Name {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        [Required(ErrorMessage = "رقم التسجيل الضريبي مطلوب")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "رقم التسجيل الضريبي يجب أن يتكون من 9 أرقام")]
        public string TaxRegistrationNumber {
            get => _taxRegistrationNumber;
            set => SetProperty(ref _taxRegistrationNumber, value);
        }
        public string TaxFileNumber {
            get => _taxFileNumber;
            set => SetProperty(ref _taxFileNumber, value);
        }
        public string EntityType { 
            get=> _entityType;
            set => SetProperty(ref _entityType, value);
        }
        public string TaxOfficeName { 
            get => _taxOfficeName;
            set => SetProperty(ref _taxOfficeName, value);
        }
        [Required(ErrorMessage = "العنوان مطلوب")]
        public string Address { 
            get => _address;
            set => SetProperty(ref _address, value);
        }
        public ICollection<OwnerViewModel> Owners { get; set; } = new ObservableCollection<OwnerViewModel>();
        public ICollection<EmailViewModel> Emails { get; set; } = new ObservableCollection<EmailViewModel>();

        public bool IsValid => ValidateAll();
    }
}
