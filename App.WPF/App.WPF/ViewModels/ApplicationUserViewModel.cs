using App.Entities.Enums;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.ViewModels
{
    public class ApplicationUserViewModel : BaseViewModel
    {

        #region Private Fields
        private string _name;
        private string _email;
        private string _password;
        private string _confirmPassword;
        private UserType _userType;
        #endregion
        public int Id { get; set; }
        [Required(ErrorMessage = "اسم المستخدم مطلوب")]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage = "يجب ادخال بريد الكتروني بشكل صحيح")]
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }
        [Required]
        [Range(1,100,ErrorMessage = "من فضلك يجب اختيار صلاحية للمستخدم")]
        public UserType UserType
        {
            get => _userType;
            set => SetProperty(ref _userType, value);
        }
        public bool IsValid => ValidateAll();
    }
}
