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
                EmailAddress = emailDTO.EmailAddress,
                Password = emailDTO.Password,
                OrganizationId = emailDTO.OrganizationId,
            };
        }
        public static List<Email> ToModel(this List<EmailDTO> emailDTOs)
        {
            if (emailDTOs == null) return null;

            return emailDTOs.Select(x => ToModel(x)).ToList();
        }


        public static void ToModel(this EmailDTO emailDTO,Email email)
        {
            email.EmailAddress = emailDTO.EmailAddress;
            email.Password = emailDTO.Password;
            email.OrganizationId = emailDTO.OrganizationId;
        }



        public static void ToModel(this ICollection<EmailDTO> emailDTOs, ICollection<Email> emails)
        {

            var emailsDictionary = emails.ToDictionary(x => x.EmailAddress);
            foreach (var emailDTO in emailDTOs)
            {
                if (!emailsDictionary.TryGetValue(emailDTO.EmailAddress, out Email email))
                {
                    // new
                    emails.Add(emailDTO.ToModel());
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
                if (!emailDTOs.Any(emailDTO => emailDTO.EmailAddress == email.EmailAddress))
                {
                    emails.Remove(email);
                }
            }
        }
        #endregion
    }
}
