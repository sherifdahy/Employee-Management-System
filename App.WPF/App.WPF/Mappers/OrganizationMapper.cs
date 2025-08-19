using App.BLL.DTOs;
using App.Entities.Models;
using MyApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.Mappers
{
    public static class OrganizationMapper
    {
        #region ViewModel => Model
        public static Organization ToModel(this OrganizationViewModel vm,Organization organization)
        {
            if (vm == null) return null;

            organization.Name = vm.Name;
            organization.URL = vm.URL;

            vm.Selectors.ToModel(organization.Selectors);
            
            return organization;
        }
        #endregion

        #region Model => ViewModel

        public static OrganizationViewModel ToViewModel(this Organization organization,OrganizationViewModel organizationViewModel)
        {
            if (organization == null) return null;

            organizationViewModel.Guid = organization.Guid;
            organizationViewModel.Name = organization.Name;
            organizationViewModel.URL = organization.URL;

            organization.Selectors.ToViewModel(organizationViewModel.Selectors);
            
            return organizationViewModel;
        }
        #endregion


        
    }
}
