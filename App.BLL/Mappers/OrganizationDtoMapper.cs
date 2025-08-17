using App.BLL.DTOs;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Mappers
{
    public static class OrganizationDtoMapper
    {
        #region Model => DTO
        public static OrganizationDTO ToDTO(this Organization organization)
        {
            if (organization == null) return null;

            return new OrganizationDTO()
            {
                Id = organization.Id,
                //CreatedAt = organization.CreatedAt,
                //UpdatedAt = organization.UpdatedAt,
                URL = organization.URL,
                Name = organization.Name,
                Selectors = organization.Selectors.ToDTO().ToList(),
            };
        }

        public static IEnumerable<OrganizationDTO> ToDTO(this IEnumerable<Organization> organizations)
        {
            if (organizations == null) throw new ArgumentNullException(nameof(organizations));

            return organizations.Select(x => ToDTO(x));
        }
        #endregion

        #region DTO => Model

        public static void ToModel(this OrganizationDTO organizationDTO,Organization organization)
        {
            organization.URL = organizationDTO.URL;
            organization.Name = organizationDTO.Name;
            organization.Selectors = organizationDTO.Selectors.ToModel();
            organization.UpdatedAt = DateTime.UtcNow;
        }

        public static Organization ToModel(this OrganizationDTO organizationDTO)
        {
            if (organizationDTO == null) return null;
            return new Organization()
            {
                Id = organizationDTO.Id,
                Name = organizationDTO.Name,
                URL = organizationDTO.URL,
                Selectors = organizationDTO.Selectors.ToModel(),
            };
        }

        #endregion
    }
}
