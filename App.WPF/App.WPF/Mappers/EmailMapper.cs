using App.Entities.Models;
using MyApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.Mappers
{
    public static class EmailMapper
    {
        #region Model => ViewModel
        public static EmailViewModel ToViewModel(this Email email)
        {
            if (email == null) throw new ArgumentNullException(nameof(email));
            return new EmailViewModel()
            {
                Id = email.Id,
                EmailAddress = email.EmailAddress,
                OrganizationId = email.OrganizationId,
                Password = email.Password,
            };
        }
        public static ICollection<EmailViewModel> ToViewModel(this ICollection<Email> emailViewModels)
        {
            if (emailViewModels == null) throw new ArgumentNullException(nameof(emailViewModels));
            return emailViewModels.Select(x => ToViewModel(x)).ToList();
        } 
        #endregion

        #region ViewModel => Model
        public static Email ToModel(this EmailViewModel emailViewModel)
        {
            if (emailViewModel == null) throw new ArgumentNullException(nameof(emailViewModel));

            return new Email()
            {
                EmailAddress = emailViewModel.EmailAddress,
                Password = emailViewModel.Password,
                OrganizationId = emailViewModel.OrganizationId,
            };
        }
        public static void ToModel(this EmailViewModel emailViewModel, Email email)
        {
            if (emailViewModel == null) throw new ArgumentNullException(nameof(emailViewModel));
            if (email == null) throw new ArgumentNullException(nameof(email));

            email.EmailAddress = emailViewModel.EmailAddress;
            email.Password = emailViewModel.Password;
            email.OrganizationId = emailViewModel.OrganizationId;
        }
        public static void ToModel(this ICollection<EmailViewModel> vms, ICollection<Email> emails)
        {
            if (vms is null) throw new ArgumentNullException(nameof(vms));
            if (emails is null) throw new ArgumentNullException(nameof(emails));

            RemoveDeletedEmails(vms, emails);
            UpdateExistingEmails(vms, emails);
            AddNewEmails(vms, emails);
        }
        #endregion

        #region ViewModel => ViewModel
        public static void ToViewModel(this EmailViewModel from,EmailViewModel to)
        {
            if(from is null) throw new ArgumentNullException(nameof(from));
            if (to is null) throw new ArgumentNullException(nameof(to));

            to.EmailAddress = from.EmailAddress;
            to.Password = from.Password;
            to.OrganizationId = from.OrganizationId;
            to.Id = from.Id;
        }
        #endregion

        #region Clone
        public static EmailViewModel Clone(this EmailViewModel emailViewModel)
        {
            return new EmailViewModel
            {
                Id = emailViewModel.Id,
                OrganizationId = emailViewModel.OrganizationId,
                EmailAddress = emailViewModel.EmailAddress,
                Password = emailViewModel.Password,
            };
        }
        #endregion

        #region Helper
        private static void RemoveDeletedEmails(ICollection<EmailViewModel> vms, ICollection<Email> emails)
        {
            var vmIds = new HashSet<int>(vms.Where(vm => vm.Id != 0).Select(vm => vm.Id));
            var toRemove = emails.Where(e => !vmIds.Contains(e.Id)).ToList();
            foreach (var email in toRemove)
                emails.Remove(email);
        }
        private static void UpdateExistingEmails(ICollection<EmailViewModel> vms, ICollection<Email> emails)
        {
            var emailDic = emails.ToDictionary(e => e.Id);
            foreach (var vm in vms.Where(vm => vm.Id != 0))
            {
                if (emailDic.TryGetValue(vm.Id, out var email))
                {
                    vm.ToModel(email);
                }
            }
        }
        private static void AddNewEmails(ICollection<EmailViewModel> vms, ICollection<Email> emails)
        {
            foreach (var vm in vms.Where(vm => vm.Id == 0))
            {
                emails.Add(ToModel(vm));
            }
        } 
        #endregion

    }
}
