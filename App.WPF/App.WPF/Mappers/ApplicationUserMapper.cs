using App.BLL.DTOs;
using App.Entities.Models;
using MyApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.Mappers
{
    public static class ApplicationUserMapper
    {
        #region Model => ViewModel
        public static ApplicationUserViewModel ToViewModel(this ApplicationUser user)
        {
            if(user is null) throw new ArgumentNullException(nameof(user));
            
            return new ApplicationUserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                UserType = user.UserType,
                Email = user.Email,
                Password = user.Password,
                ConfirmPassword = user.Password,
            };
        }
        #endregion

        #region ViewModel => Model
        public static void ToModel(this ApplicationUserViewModel from , ApplicationUser to)
        {
            if (from == null) throw new ArgumentNullException(nameof(from));
            if (to == null) throw new ArgumentNullException(nameof(to));
        
            to.Email = from.Email;
            to.Password = from.Password;
            to.UserType = from.UserType;
            to.Name = from.Name;
        }
        public static ApplicationUser ToModel(this ApplicationUserViewModel vm)
        {
            if (vm == null) throw new ArgumentNullException(nameof(vm));

            return new ApplicationUser()
            {
                Email = vm.Email,
                Name = vm.Name,
                Password = vm.Password,
                UserType= vm.UserType,
            };
        }
        #endregion

        
    }
}
