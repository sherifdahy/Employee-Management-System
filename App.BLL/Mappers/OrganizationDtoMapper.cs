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
        public static OrganizationDTO ToDTO(this Organization organization, OrganizationDTO organizationDTO)
        {
            if (organization == null || organizationDTO is null) return null;

            organizationDTO.URL = organization.URL;
            organizationDTO.Name = organization.Name;

            if (organizationDTO.Selectors == null)
                organizationDTO.Selectors = new List<SelectorDTO>();

            organization.Selectors.ToDTO(organizationDTO.Selectors);

            return organizationDTO;
        }
        #endregion

        #region DTO => Model
        public static Organization ToModel(this OrganizationDTO organizationDTO, Organization organization)
        {
            if (organizationDTO == null || organization == null) return null;

            organization.URL = organizationDTO.URL;
            organization.Name = organizationDTO.Name;

            if (organization.Selectors == null)
                organization.Selectors = new List<Selector>();

            organizationDTO.Selectors.ToModel(organization.Selectors);

            return organization;
        }
        #endregion
    }

}
