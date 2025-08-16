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
        public static Organization ToModel(this OrganizationViewModel vm)
        {
            if (vm == null) throw new ArgumentNullException(nameof(vm));

            return new Organization()
            {
                Name = vm.Name,
                URL = vm.URL,
                Selectors = vm.Selectors.ToModel().ToList(),
            };
        }
        #endregion

        #region Model => ViewModel

        public static OrganizationViewModel ToViewModel(this Organization organization)
        {
            if (organization == null) throw new ArgumentNullException(nameof(organization));

            return new OrganizationViewModel()
            {
                Name = organization.Name,
                URL = organization.URL,
                Selectors = organization.Selectors.ToViewModel().ToList(),
            };
        }
        #endregion


        
    }
}
