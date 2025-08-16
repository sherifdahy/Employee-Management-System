using App.BLL.DTOs;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Mappers
{
    public static class OwnerDtoMapper
    {
        #region Model => DTO
        public static OwnerDTO ToDTO(this Owner owner)
        {
            if (owner == null) return null;

            return new OwnerDTO()
            {
                Id = owner.Id,
                Name = owner.Name,
                Address = owner.Address,
                PhoneNumber = owner.PhoneNumber,
                NationalId = owner.NationalId,
                CompanyId = owner.CompanyId,
            };
        }

        public static IEnumerable<OwnerDTO> ToDTO(this IEnumerable<Owner> owners)
        {
            if (owners == null) throw new ArgumentNullException(nameof(owners));

            return owners.Select(x => ToDTO(x));
        }
        #endregion


        #region DTO => Model
        public static Owner ToModel(this OwnerDTO ownerDTO)
        {
            if (ownerDTO == null) return null;

            return new Owner()
            {
                Id = ownerDTO.Id,
                Name = ownerDTO.Name,
                Address = ownerDTO.Address,
                PhoneNumber = ownerDTO.PhoneNumber,
                NationalId = ownerDTO.NationalId,
                CompanyId = ownerDTO.CompanyId,
            };
        }
        public static List<Owner> ToModel(this List<OwnerDTO> ownerDTOs)
        {
            if (ownerDTOs == null) return null;

            return ownerDTOs.Select(x => ToModel(x)).ToList();
        }
        #endregion

    }
}
