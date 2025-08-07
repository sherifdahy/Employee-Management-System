using App.Entities.Models;
using Azure.Core;
using MyApp.WPF.ViewModels;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.Mappers
{
    public static class OwnerMapper
    {
        public static OwnerViewModel ToViewModel(this Owner owner)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            return new OwnerViewModel
            {
                Id = owner.Id,
                Address = owner.Address,
                Name = owner.Name,
                NationalId = owner.NationalId,
                PhoneNumber = owner.PhoneNumber,
            };

        }

        public static ICollection<OwnerViewModel> ToViewModel(this ICollection<Owner> owners)
        {
            if (owners == null) throw new ArgumentNullException(nameof(owners));
            return owners.Select(o => ToViewModel(o)).ToList();
        }
        #region New
        public static Owner ToModel(this OwnerViewModel vm)
        {
            if (vm == null) throw new ArgumentNullException(nameof(vm));

            return new Owner()
            {
                Name = vm.Name,
                NationalId = vm.NationalId,
                PhoneNumber = vm.PhoneNumber,
                Address = vm.Address,
            };
        }
        #endregion

        /// <summary>
        /// Updates the existing entity by modifying its properties directly.
        /// Since the entity is a reference type, changes are applied in-place,
        /// so there is no need to return a value.
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="owner"></param>
        /// <exception cref="ArgumentNullException"></exception>
        #region Update

        public static void ToModel(this OwnerViewModel vm, Owner owner)
        {
            if (vm == null) throw new ArgumentNullException(nameof(vm));
            if (owner == null) throw new ArgumentNullException(nameof(owner));

            owner.Name = vm.Name;
            owner.NationalId = vm.NationalId;
            owner.PhoneNumber = vm.PhoneNumber;
            owner.Address = vm.Address;
        }
        public static void ToModel(this ICollection<OwnerViewModel> vms, ICollection<Owner> owners)
        {
            if (vms is null) throw new ArgumentNullException(nameof(vms));
            if (owners is null) throw new ArgumentNullException(nameof(owners));

            RemoveDeletedEmails(vms, owners);
            UpdateExistingEmails(vms, owners);
            AddNewEmails(vms, owners);
        }

        private static void RemoveDeletedEmails(ICollection<OwnerViewModel> vms, ICollection<Owner> owners)
        {
            var vmIds = new HashSet<int>(vms.Where(vm => vm.Id != 0).Select(vm => vm.Id));
            var toRemove = owners.Where(e => !vmIds.Contains(e.Id)).ToList();
            foreach (var owner in toRemove)
                owners.Remove(owner);
        }

        private static void UpdateExistingEmails(ICollection<OwnerViewModel> vms, ICollection<Owner> owners)
        {
            var ownerDic = owners.ToDictionary(e => e.Id);
            foreach (var vm in vms.Where(vm => vm.Id != 0))
            {
                if (ownerDic.TryGetValue(vm.Id, out var owner))
                {
                    vm.ToModel(owner);
                }
            }
        }

        private static void AddNewEmails(ICollection<OwnerViewModel> vms, ICollection<Owner> owners)
        {
            foreach (var vm in vms.Where(vm => vm.Id == 0))
            {
                owners.Add(ToModel(vm));
            }
        }
        #endregion
    }
}
