using App.BLL.DTOs;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                CreatedAt = company.CreatedAt,
                UpdatedAt = company.UpdatedAt,
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



        #region DTO => Model
        public static Company ToModel(this CompanyDTO companyDTO)
        {
            if (companyDTO is null) return null;
            return new Company()
            {
                Id = companyDTO.Id,
                IsDeleted = companyDTO.IsDeleted,
                CreatedAt= companyDTO.CreatedAt,
                UpdatedAt= companyDTO.UpdatedAt,
                Address= companyDTO.Address,
                Name = companyDTO.Name,
                EntityType= companyDTO.EntityType,
                TaxFileNumber= companyDTO.TaxFileNumber,
                TaxOfficeName= companyDTO.TaxOfficeName,
                TaxRegistrationNumber= companyDTO.TaxRegistrationNumber,
                Owners = companyDTO.Owners.ToModel(),
                Emails = companyDTO.Emails.ToModel(),
            };
        }

        public static void ToModel(this CompanyDTO companyDTO,Company company)
        {
            company.Name = companyDTO.Name;
            company.CreatedAt = companyDTO.CreatedAt;
            company.UpdatedAt = companyDTO.UpdatedAt;
            company.Address = companyDTO.Address;
            company.EntityType = companyDTO.EntityType;
            company.IsDeleted = companyDTO.IsDeleted;
            company.EntityType = companyDTO.EntityType;
            company.TaxFileNumber = companyDTO.TaxFileNumber;
            company.TaxOfficeName = companyDTO.TaxOfficeName;
            company.TaxRegistrationNumber = companyDTO.TaxRegistrationNumber;
            company.Owners = companyDTO.Owners.ToModel();
            company.Emails = companyDTO.Emails.ToModel();
        }
        #endregion
    }
}
