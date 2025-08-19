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
        public static Company ToModel(this CompanyViewModel vm, Company company)
        {
            if (vm is null || company is null) return null;

            company.Name = vm.Name;
            company.TaxRegistrationNumber = vm.TaxRegistrationNumber;
            company.TaxFileNumber = vm.TaxFileNumber;
            company.EntityType = vm.EntityType;
            company.TaxOfficeName = vm.TaxOfficeName;
            company.Address = vm.Address;
            
            vm.Owners.ToModel(company.Owners);
            vm.Emails.ToModel(company.Emails);

            return company;
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
            company.Owners.ToViewModel(compVM.Owners);
            //company.Emails.Where(e=>e.Organization?.Name != null).GroupBy(x => x.Organization?.Name).ToDictionary(g => g.Key, g => g.ToList().ToViewModel(compVM.Emails));
            return compVM;
        }


        public static CompanyViewModel ToViewModel(this Company company,CompanyViewModel companyViewModel)
        {
            if (company is null || companyViewModel is null) return null;

            companyViewModel.Id = company.Id;
            companyViewModel.Name = company.Name;
            companyViewModel.Address = company.Address;
            companyViewModel.EntityType = company.EntityType;
            companyViewModel.TaxRegistrationNumber= company.TaxRegistrationNumber;
            companyViewModel.TaxFileNumber= company.TaxFileNumber;

            company.Emails.ToViewModel(companyViewModel.Emails);
            company.Owners.ToViewModel(companyViewModel.Owners);

            return companyViewModel;
        }
        #endregion

        
    }
}


