using App.BLL.DTOs;
using App.Entities.Models;

namespace App.BLL.Mappers
{
    public static class CompanyDtoMapper
    {
        #region Model => DTO
        public static CompanyDTO ToDTO(this Company company, CompanyDTO companyDTO)
        {
            if (company is null || companyDTO is null) return null;

            companyDTO.IsDeleted = company.IsDeleted;
            companyDTO.Address = company.Address;
            companyDTO.EntityType = company.EntityType;
            companyDTO.Name = company.Name;
            companyDTO.TaxFileNumber = company.TaxFileNumber;
            companyDTO.TaxOfficeName = company.TaxOfficeName;
            companyDTO.TaxRegistrationNumber = company.TaxRegistrationNumber;

            company.Emails.ToDTO(companyDTO.Emails);
            company.Owners.ToDTO(companyDTO.Owners);

            return companyDTO;
        }
        #endregion

        #region DTO => Model
        public static Company ToModel(this CompanyDTO companyDTO,Company company)
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

            return company;
        }
        #endregion
    }
}
