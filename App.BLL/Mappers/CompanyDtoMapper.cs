using App.BLL.DTOs;
using App.Entities.Models;
using Interfaces;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace App.BLL.Mappers
{
    public static class CompanyDtoMapper
    {
        #region Model => DTO
        public static CompanyDTO ToDTO(this Company company)
        {
            if (company is null) return null;
            return new CompanyDTO()
            {
                Id = company.Id,
                IsDeleted = company.IsDeleted,
                //CreatedAt = company.CreatedAt,
                //UpdatedAt = company.UpdatedAt,
                Address = company.Address,
                EntityType = company.EntityType,
                Name = company.Name,
                TaxFileNumber = company.TaxFileNumber,
                TaxOfficeName = company.TaxOfficeName,
                TaxRegistrationNumber = company.TaxRegistrationNumber,
                Emails = company.Emails.ToDTO().ToList(),
                Owners = company.Owners.ToDTO().ToList(),
            };
        }

        public static IEnumerable<CompanyDTO> ToDTO(this IEnumerable<Company> companies)
        {
            if (companies is null) throw new ArgumentNullException(nameof(companies));
        
            return companies.Select(x => ToDTO(x));
        }
        #endregion


        /*

         #region Model => DTO
                public static void ToDTO(this Company company,CompanyDTO companyDTO)
                {
                    if (company is null || companyDTO is null) return;

                    companyDTO.Id = company.Id;
                    companyDTO.IsDeleted = company.IsDeleted;
                    companyDTO.Address = company.Address;
                    companyDTO.EntityType = company.EntityType;
                    companyDTO.Name = company.Name;
                    companyDTO.TaxFileNumber = company.TaxFileNumber;
                    companyDTO.TaxOfficeName = company.TaxOfficeName;
                    companyDTO.TaxRegistrationNumber = company.TaxRegistrationNumber;

                    companyDTO.Emails = company.Emails.ToDTO().ToList();
                    companyDTO.Owners = company.Owners.ToDTO().ToList();
                }
                #endregion
         */


        #region DTO => Model
        public static void ToModel(this CompanyDTO companyDTO,Company company)
        {
            company.Name = companyDTO.Name;
            company.Address = companyDTO.Address;
            company.EntityType = companyDTO.EntityType;
            company.IsDeleted = companyDTO.IsDeleted;
            company.EntityType = companyDTO.EntityType;
            company.TaxFileNumber = companyDTO.TaxFileNumber;
            company.TaxOfficeName = companyDTO.TaxOfficeName;
            company.TaxRegistrationNumber = companyDTO.TaxRegistrationNumber;

            companyDTO.Owners.ToModel(company.Owners);
            companyDTO.Emails.ToModel(company.Emails);
        }
        #endregion
    }
}
