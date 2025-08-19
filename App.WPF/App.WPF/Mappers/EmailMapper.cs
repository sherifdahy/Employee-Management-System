using App.Entities.Models;
using MyApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApp.WPF.Mappers
{
    public static class EmailMapper
    {
        #region Model => ViewModel
        public static EmailViewModel ToViewModel(this Email email, EmailViewModel vm)
        {
            if (email is null || vm is null) return null;

            vm.Id = email.Id;
            vm.EmailAddress = email.EmailAddress;
            vm.OrganizationId = email.OrganizationId;
            vm.Password = email.Password;

            return vm;
        }

        public static ICollection<EmailViewModel> ToViewModel(this ICollection<Email> emails, ICollection<EmailViewModel> vms)
        {
            if (emails is null || vms is null) return null;

            var vmsDictionary = vms.ToDictionary(x => (x.EmailAddress,x.OrganizationId));
            foreach (var email in emails)
            {
                if (!vmsDictionary.TryGetValue((email.EmailAddress,email.OrganizationId), out var vm))
                {
                    // new
                    vms.Add(email.ToViewModel(new EmailViewModel()));
                }
                else
                {
                    // exist -> update
                    email.ToViewModel(vm);
                }
            }

            foreach (var vm in vms.ToList())
            {
                // remove not exist
                if (!emails.Any(e => (e.EmailAddress,e.OrganizationId) == (vm.EmailAddress,vm.OrganizationId)))
                {
                    vms.Remove(vm);
                }
            }

            return vms;
        }
        #endregion

        #region ViewModel => Model
        public static Email ToModel(this EmailViewModel vm, Email email)
        {
            if (vm is null || email is null) return null;

            email.EmailAddress = vm.EmailAddress;
            email.Password = vm.Password;
            email.OrganizationId = vm.OrganizationId;

            return email;
        }

        public static ICollection<Email> ToModel(this ICollection<EmailViewModel> vms, ICollection<Email> emails)
        {
            if (vms is null || emails is null) return null;

            var emailsDictionary = emails.ToDictionary(x => (x.EmailAddress,x.OrganizationId));
            foreach (var vm in vms)
            {
                if (!emailsDictionary.TryGetValue((vm.EmailAddress,vm.OrganizationId), out var email))
                {
                    // new
                    emails.Add(vm.ToModel(new Email()));
                }
                else
                {
                    // exist -> update
                    vm.ToModel(email);
                }
            }

            foreach (var model in emails.ToList())
            {
                // remove not exist
                if (!vms.Any(vm => (vm.EmailAddress,vm.OrganizationId) == (model.EmailAddress,model.OrganizationId)))
                {
                    emails.Remove(model);
                }
            }

            return emails;
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
    }
}
