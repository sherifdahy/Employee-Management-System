using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        private string _password;

        [Required(ErrorMessage = "اسم المستخدم مطلوب")]
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        // خاصية للتحقق من صحة النموذج بالكامل
        public bool IsValid => ValidateAll();
    }

    
}
