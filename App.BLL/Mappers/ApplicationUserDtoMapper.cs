using App.BLL.DTOs;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Mappers
{
    public static class ApplicationUserDtoMapper
    {
        #region Model => DTO
        public static ApplicationUserDTO ToDTO(this ApplicationUser applicationUser, ApplicationUserDTO applicationUserDTO)
        {
            if (applicationUser == null || applicationUserDTO == null) return null;

            applicationUserDTO.IsDeleted = applicationUser.IsDeleted;
            applicationUserDTO.Email = applicationUser.Email;
            applicationUserDTO.Name = applicationUser.Name;
            applicationUserDTO.Password = applicationUser.Password;
            applicationUserDTO.UserType = applicationUser.UserType;

            applicationUser.Account?.ToDTO(applicationUserDTO.Account);

            return applicationUserDTO;
        }
        #endregion

        #region DTO => Model
        public static ApplicationUser ToModel(this ApplicationUserDTO applicationUserDTO, ApplicationUser applicationUser)
        {
            if (applicationUserDTO == null || applicationUser == null) return null;

            applicationUser.Name = applicationUserDTO.Name;
            applicationUser.IsDeleted = applicationUserDTO.IsDeleted;
            applicationUser.UserType = applicationUserDTO.UserType;
            applicationUser.Email = applicationUserDTO.Email;
            applicationUser.Password = applicationUserDTO.Password;

            applicationUserDTO.Account?.ToModel(applicationUser.Account);

            return applicationUser;
        }
        #endregion
    }

}
