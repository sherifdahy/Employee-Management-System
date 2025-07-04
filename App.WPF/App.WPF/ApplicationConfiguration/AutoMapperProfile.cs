using App.Entities.Models;
using AutoMapper;
using MyApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.ApplicationConfiguration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Company
            CreateMap<CompanyViewModel, Company>().ReverseMap();
            #endregion
            #region Email
            CreateMap<EmailViewModel,Email>().ReverseMap();
            #endregion
            #region Owner
            CreateMap<OwnerViewModel, Owner>().ReverseMap();
            #endregion
            #region Organization
            CreateMap<OrganizationViewModel, Organization>().ReverseMap();
            #endregion
            #region Employee
            CreateMap<RegisterViewModel, ApplicationUser>().ReverseMap();
            #endregion
        }
    }
}
