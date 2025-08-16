using App.BLL;
using App.Entities.Models;
using AutoMapper;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MyApp.WPF.Mappers;
using MyApp.WPF.Services.Dialog;
using MyApp.WPF.ViewModels;
using MyApp.WPF.Windows.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telerik.Windows.Controls.Diagrams;

namespace MyApp.WPF.UserControls.Admin.Organizations
{
    public partial class NewOrganizationControl : UserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IOrganizationService _organizationService;
        public NewOrganizationControl(IOrganizationService organizationService,IMapper mapper,IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _organizationService = organizationService;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.FormControl.Content = ActivatorUtilities.CreateInstance<FormOrganizationControl>(_serviceProvider);
            this.FormControl.DataContext = new OrganizationViewModel();
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var formControl = this.FormControl.Content as FormOrganizationControl;
            if (formControl == null)
            {
                DialogService.ShowError("خطأ داخلي");
                return;
            }

            var organizationVM = formControl.DataContext as OrganizationViewModel;
            if (!organizationVM.IsValid)
                return;

            var organization = organizationVM.ToModel();
            await _organizationService.CreateAsync(organization);

            DialogService.ShowSuccess("تم الاضافة بنجاح.");
            
            this.Content = ActivatorUtilities.CreateInstance<OrganizationsControl>(_serviceProvider);
        }


    }
}
