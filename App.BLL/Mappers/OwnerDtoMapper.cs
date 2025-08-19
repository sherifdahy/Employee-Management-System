using App.BLL.DTOs;
using App.Entities.Models;
using Interfaces;
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
        public static OwnerDTO ToDTO(this Owner owner, OwnerDTO ownerDTO)
        {
            if (owner == null || ownerDTO is null) return null;

            ownerDTO.Name = owner.Name;
            ownerDTO.Address = owner.Address;
            ownerDTO.PhoneNumber = owner.PhoneNumber;
            ownerDTO.NationalId = owner.NationalId;

            return ownerDTO;
        }

        public static ICollection<OwnerDTO> ToDTO(this ICollection<Owner> owners, ICollection<OwnerDTO> ownerDTOs)
        {
            if (owners == null || ownerDTOs == null) return null;

            var dtoDictionary = ownerDTOs.ToDictionary(x => x.NationalId);

            foreach (var owner in owners)
            {
                if (!dtoDictionary.TryGetValue(owner.NationalId, out var dto))
                {
                    // new
                    ownerDTOs.Add(owner.ToDTO(new OwnerDTO()));
                }
                else
                {
                    // exist
                    owner.ToDTO(dto);
                }
            }

            // remove not exist
            foreach (var dto in ownerDTOs.ToList())
            {
                if (!owners.Any(owner => owner.NationalId == dto.NationalId))
                {
                    ownerDTOs.Remove(dto);
                }
            }

            return ownerDTOs;
        }
        #endregion

        #region DTO => Model
        public static Owner ToModel(this OwnerDTO ownerDTO, Owner owner)
        {
            if (ownerDTO is null || owner is null) return null;

            owner.Name = ownerDTO.Name;
            owner.Address = ownerDTO.Address;
            owner.PhoneNumber = ownerDTO.PhoneNumber;
            owner.NationalId = ownerDTO.NationalId;

            return owner;
        }

        public static void ToModel(this ICollection<OwnerDTO> ownersDTO, ICollection<Owner> owners)
        {
            var ownersDictionary = owners.ToDictionary(x => x.NationalId);
            foreach (var ownerDTO in ownersDTO)
            {
                if (!ownersDictionary.TryGetValue(ownerDTO.NationalId, out var owner))
                {
                    // new
                    owners.Add(ownerDTO.ToModel(new Owner()));
                }
                else
                {
                    // exist
                    ownerDTO.ToModel(owner);
                }
            }

            foreach (var owner in owners.ToList())
            {
                // remove not exist
                if (!ownersDTO.Any(ownerDTO => ownerDTO.NationalId == owner.NationalId))
                {
                    owners.Remove(owner);
                }
            }
        }
        #endregion
    }

}
