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
        #region Model => DTO
        public static EmailDTO ToDTO(this Email email, EmailDTO emailDTO)
        {
            if (email == null || emailDTO is null) return null;

            emailDTO.EmailAddress = email.EmailAddress;
            emailDTO.Password = email.Password;
            emailDTO.OrganizationId = email.OrganizationId;

            return emailDTO;
        }

        public static ICollection<EmailDTO> ToDTO(this ICollection<Email> emails, ICollection<EmailDTO> emailDTOs)
        {
            if (emails == null || emailDTOs == null) return null;

            var dtoDictionary = emailDTOs.ToDictionary(x => (x.EmailAddress,x.OrganizationId));

            foreach (var email in emails)
            {
                if (!dtoDictionary.TryGetValue((email.EmailAddress,email.OrganizationId), out var dto))
                {
                    // new
                    emailDTOs.Add(email.ToDTO(new EmailDTO()));
                }
                else
                {
                    // exist
                    email.ToDTO(dto);
                }
            }

            // remove not exist
            foreach (var dto in emailDTOs.ToList())
            {
                if (!emails.Any(email => (email.EmailAddress,email.OrganizationId) == (dto.EmailAddress,dto.OrganizationId)))
                {
                    emailDTOs.Remove(dto);
                }
            }

            return emailDTOs;
        }
        #endregion

        #region DTO => Model
        public static Email ToModel(this EmailDTO emailDTO, Email email)
        {
            if (emailDTO is null || email is null) return null;

            email.EmailAddress = emailDTO.EmailAddress;
            email.Password = emailDTO.Password;
            email.OrganizationId = emailDTO.OrganizationId;

            return email;
        }

        public static ICollection<Email> ToModel(this ICollection<EmailDTO> emailDTOs, ICollection<Email> emails)
        {
            if (emailDTOs is null || emails is null) return null;

            var emailsDictionary = emails.ToDictionary(x => (x.EmailAddress,x.OrganizationId));
            foreach (var emailDTO in emailDTOs)
            {
                if (!emailsDictionary.TryGetValue((emailDTO.EmailAddress,emailDTO.OrganizationId), out var email))
                {
                    // new
                    emails.Add(emailDTO.ToModel(new Email()));
                }
                else
                {
                    // exist
                    emailDTO.ToModel(email);
                }
            }

            foreach (var email in emails.ToList())
            {
                // remove not exist
                if (!emailDTOs.Any(emailDTO => (emailDTO.EmailAddress,emailDTO.OrganizationId) == (email.EmailAddress,email.OrganizationId)))
                {
                    emails.Remove(email);
                }
            }

            return emails;
        }
        #endregion
    }

}
