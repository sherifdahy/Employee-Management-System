using App.Entities.Models;
using MyApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApp.WPF.Mappers
{
    public static class CompanyMapper
    {
        #region from ViewModel

        public static Company ToModel(this CompanyViewModel vm)
        {
            if (vm == null) return null;

            return new Company()
            {

            };
        }

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
            vm.Owners.ToModel(company.Owners);
            vm.Emails.ToModel(company.Emails);
        }

        #endregion


        #region from Model
        public static CompanyViewModel ToViewModel(this Company company)
        {
            if(company is null) return null;
            
            return new CompanyViewModel()
            {

            };
        }
        public static DisplayCompanyViewModel ToDisplayViewModel(this Company company) 
        {
            if(company is null) throw new ArgumentNullException(nameof(company));

            return new DisplayCompanyViewModel()
            {
                Name = company.Name,
                TaxRegistrationNumber = company.TaxRegistrationNumber,
                TaxFileNumber = company.TaxFileNumber,
                EntityType = company.EntityType,
                Address = company.Address,
                Owners = company.Owners.ToViewModel(),
                Emails = company.Emails.GroupBy(x => x.Organization.Name).ToDictionary(g => g.Key, g =>g.ToList().ToViewModel())
            };
        }
        #endregion
    }
}


