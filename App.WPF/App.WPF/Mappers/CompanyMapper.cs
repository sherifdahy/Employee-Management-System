using App.BLL.DTOs;
using App.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApp.WPF.Mappers
{
    public static class CompanyMapper
    {
        #region ViewModel => Model
        public static void ToModel(this CompanyViewModel vm, Company company)
        {
            if (vm == null) throw new ArgumentNullException(nameof(vm));
            if (company == null) throw new ArgumentNullException(nameof(company));

            company.Name = vm.Name;
            company.TaxRegistrationNumber = vm.TaxRegistrationNumber;
            company.TaxFileNumber = vm.TaxFileNumber;
            company.EntityType = vm.EntityType;
            company.TaxOfficeName = vm.TaxOfficeName;
            company.Address = vm.Address;
            //vm.Owners.ToModel(company.Owners);
            vm.Emails.ToModel(company.Emails,company.Id);
        }
        #endregion

        #region Model => ViewModel
        public static DisplayCompanyViewModel ToDisplayViewModel(this Company company) 
        {
            if(company is null) throw new ArgumentNullException(nameof(company));

            var compVM = new DisplayCompanyViewModel();

            compVM.Name = company.Name;
            compVM.TaxRegistrationNumber = company.TaxRegistrationNumber;
            compVM.TaxFileNumber = company.TaxFileNumber;
            compVM.EntityType = company.EntityType;
            compVM.Address = company.Address;
            compVM.Owners = company.Owners.ToViewModel();
            compVM.Emails = company.Emails.Where(e=>e.Organization?.Name != null).GroupBy(x => x.Organization?.Name).ToDictionary(g => g.Key, g => g.ToList().ToViewModel());
            return compVM;
        }
        #endregion

        
    }
}


