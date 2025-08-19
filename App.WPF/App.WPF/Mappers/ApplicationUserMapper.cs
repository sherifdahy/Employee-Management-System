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
        public static ApplicationUserViewModel ToViewModel(this ApplicationUser user,ApplicationUserViewModel applicationUserViewModel)
        {

            if(user is null || applicationUserViewModel is null) return null;

            applicationUserViewModel.Id = user.Id;
            applicationUserViewModel.Name = user.Name;
            applicationUserViewModel.UserType = user.UserType;
            applicationUserViewModel.Email = user.Email;
            applicationUserViewModel.Password = user.Password;
            applicationUserViewModel.ConfirmPassword = user.Password;

            return applicationUserViewModel;
        }
        #endregion

        #region ViewModel => Model
        public static ApplicationUser ToModel(this ApplicationUserViewModel from , ApplicationUser to)
        {
            if (from is null || to is null) return null;

            to.Email = from.Email;
            to.Password = from.Password;
            to.UserType = from.UserType;
            to.Name = from.Name;

            return to;
        }
        #endregion

        
    }
}
