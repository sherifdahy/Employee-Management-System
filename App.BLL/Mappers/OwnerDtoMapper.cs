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
                Name = ownerDTO.Name,
                Address = ownerDTO.Address,
                PhoneNumber = ownerDTO.PhoneNumber,
                NationalId = ownerDTO.NationalId,
            };
        }
        public static List<Owner> ToModel(this List<OwnerDTO> ownerDTOs)
        {
            if (ownerDTOs == null) return null;

            return ownerDTOs.Select(x => ToModel(x)).ToList();
        }

        public static void ToModel(this OwnerDTO ownerDTO,Owner owner)
        {
            owner.Name = ownerDTO.Name;
            owner.Address = owner.Address;
            owner.PhoneNumber = owner.PhoneNumber;
            owner.NationalId = ownerDTO.NationalId;
        }

        public static void ToModel(this ICollection<OwnerDTO> ownersDTO,ICollection<Owner> owners)
        {

            var ownersDictionary = owners.ToDictionary(x => x.NationalId);
            foreach (var ownerDTO in ownersDTO)
            {
                if (!ownersDictionary.TryGetValue(ownerDTO.NationalId, out Owner owner))
                {
                    // new
                    owners.Add(ownerDTO.ToModel());
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

        //var ModelDictionary = owners.ToDictionary(x => x.Id);
        //var DtoDictionary = ownersDTO.ToDictionary(x => x.Id);
        //foreach(var ownerDTO in ownersDTO)
        //{
        //    if (!ModelDictionary.TryGetValue(ownerDTO.Id,out Owner owner))
        //    {
        //        // new
        //        var ownerTemp = ownerDTO.ToModel();
        //        unitOfWork.Owners.Add(ownerTemp);
        //    }
        //    else
        //    {
        //        // exist => update
        //        ownerDTO.ToModel(owner);
        //        unitOfWork.Owners.Update(owner);
        //    }
        //}
        //foreach(var owner in owners)
        //{
        //    // remove not exist
        //    if(!DtoDictionary.TryGetValue(owner.Id,out OwnerDTO ownerTemp))
        //    {
        //        unitOfWork.Owners.Delete(owner);
        //    }
        //}
        #endregion

    }
}
