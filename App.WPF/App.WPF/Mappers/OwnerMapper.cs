using App.BLL.DTOs;
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
        #region Model => ViewModel
        public static OwnerViewModel ToViewModel(this Owner owner,OwnerViewModel ownerViewModel)
        {
            if (owner is null || ownerViewModel is null) return null;

            ownerViewModel.Id = owner.Id;
            ownerViewModel.Address = owner.Address;
            ownerViewModel.Name = owner.Name;
            ownerViewModel.NationalId = owner.NationalId;
            ownerViewModel.PhoneNumber = owner.PhoneNumber;

            return ownerViewModel;
        }

        public static ICollection<OwnerViewModel> ToViewModel(this ICollection<Owner> owners, ICollection<OwnerViewModel> vms)
        {
            if (owners is null || vms is null) return null;

            var vmsDictionary = vms.ToDictionary(x => x.NationalId);
            foreach (var owner in owners)
            {
                if (!vmsDictionary.TryGetValue(owner.NationalId, out OwnerViewModel vm))
                {
                    // new
                    vms.Add(owner.ToViewModel(new OwnerViewModel()));
                }
                else
                {
                    // exist -> update
                    owner.ToViewModel(vm);
                }
            }

            foreach (var vm in vms.ToList())
            {
                // remove not exist
                if (!owners.Any(o => o.NationalId == vm.NationalId))
                {
                    vms.Remove(vm);
                }
            }

            return vms;
        }
        #endregion

        #region ViewModel => Model

        public static Owner ToModel(this OwnerViewModel vm, Owner owner)
        {
            if (vm is null || owner is null) return null;

            owner.Name = vm.Name;
            owner.NationalId = vm.NationalId;
            owner.PhoneNumber = vm.PhoneNumber;
            owner.Address = vm.Address;

            return owner;
        }
        public static ICollection<Owner> ToModel(this ICollection<OwnerViewModel> vms, ICollection<Owner> owners)
        {
            if(vms is null || owners is null) return null;

            var ownersDictionary = owners.ToDictionary(x => x.NationalId);
            foreach(var vm in vms)
            {
                if(!ownersDictionary.TryGetValue(vm.NationalId, out Owner owner))
                {
                    // new
                    owners.Add(vm.ToModel(new Owner()));
                }
                else
                {
                    // exist
                    vm.ToModel(owner);
                }
            }

            foreach (var model in owners.ToList())
            {
                // remove not exist
                if (!vms.Any(vm => vm.NationalId == model.NationalId))
                {
                    owners.Remove(model);
                }
            }

            return owners;
        }
        #endregion

    }
}
