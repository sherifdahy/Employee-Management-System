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
        public static ApplicationUserDTO ToDTO(this ApplicationUser applicationUser)
        {
            if (applicationUser == null) return null;

            return new ApplicationUserDTO()
            {
                Id = applicationUser.Id,
                IsDeleted = applicationUser.IsDeleted,
                Email = applicationUser.Email,
                Name = applicationUser.Name,
                Password = applicationUser.Password,
                UserType = applicationUser.UserType,
                CreatedAt = applicationUser.CreatedAt,
                UpdatedAt = applicationUser.UpdatedAt,
                Account = applicationUser.Account.ToDTO()
            };
        }

        public static IEnumerable<ApplicationUserDTO> ToDTO(this IEnumerable<ApplicationUser> applicationUsers)
        {
            if (applicationUsers == null) throw new ArgumentNullException(nameof(applicationUsers));

            return applicationUsers.Select(x => ToDTO(x));
        }

        #endregion

        #region DTO => Model
        public static ApplicationUser ToModel(this ApplicationUserDTO applicationUserDTO)
        {
            if (applicationUserDTO == null) return null;
            
            return new ApplicationUser()
            {
                Id = applicationUserDTO.Id,
                CreatedAt = applicationUserDTO.CreatedAt,
                UpdatedAt = applicationUserDTO.UpdatedAt,
                Name = applicationUserDTO.Name,
                Email = applicationUserDTO.Email,
                Password = applicationUserDTO.Password,
                IsDeleted = applicationUserDTO.IsDeleted,
                UserType = applicationUserDTO.UserType,
                Account = applicationUserDTO.Account.ToModel(),
                
            };
        }

        public static void ToModel(this ApplicationUserDTO applicationUserDTO,ApplicationUser applicationUser)
        {
            applicationUser.Name = applicationUserDTO.Name;
            applicationUser.IsDeleted = applicationUserDTO.IsDeleted;
            applicationUser.UserType = applicationUserDTO.UserType;
            applicationUser.Account = applicationUserDTO.Account.ToModel();
            applicationUser.Email = applicationUserDTO.Email;
            applicationUser.Password = applicationUserDTO.Password;
        }
        #endregion
    }
}
