using App.BLL.DTOs;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Mappers
{
    public static class EmailDtoMapper
    {
        #region Model => ToDTO
        public static EmailDTO ToDTO(this Email email)
        {
            if (email == null) return null;

            return new EmailDTO()
            {
                Id = email.Id,
                EmailAddress = email.EmailAddress,
                Password = email.Password,
                OrganizationId = email.OrganizationId,
                CompanyId = email.CompanyId,
            };
        }

        public static IEnumerable<EmailDTO> ToDTO(this IEnumerable<Email> emails)
        {
            if (emails == null) throw new ArgumentNullException(nameof(emails));

            return emails.Select(x => x.ToDTO());
        }
        #endregion

        #region DTO => Model
        public static Email ToModel(this EmailDTO emailDTO)
        {
            if (emailDTO == null) return null;

            return new Email()
            {
                Id = emailDTO.Id,
                EmailAddress = emailDTO.EmailAddress,
                Password = emailDTO.Password,
                OrganizationId = emailDTO.OrganizationId,
                CompanyId = emailDTO.CompanyId
            };
        }
        public static List<Email> ToModel(this List<EmailDTO> emailDTOs)
        {
            if (emailDTOs == null) return null;

            return emailDTOs.Select(x => ToModel(x)).ToList();
        }
        #endregion
    }
}
