using App.BLL;
using App.Entities;
using App.Entities.Enums;
using App.Entities.Models;
using AutoMapper;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MyApp.WPF.Mappers;
using MyApp.WPF.Services.Dialog;
using MyApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyApp.WPF.UserControls.Admin.Employees
{
    public partial class NewEmployeeControl : UserControl
    {
        private readonly IEmployeeService _employeeService;
        private readonly IServiceProvider _serviceProvider;
        public NewEmployeeControl(IServiceProvider serviceProvider,IEmployeeService employeeService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _employeeService = employeeService;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BodyContent.Content = ActivatorUtilities.CreateInstance<FormEmployeeControl>(_serviceProvider,new ApplicationUserViewModel());
        }
        private async void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            var formControl = this.BodyContent.Content as FormEmployeeControl;
            if (formControl is null)
            {
                DialogService.ShowError(ErrorCatalog.Server.Unexpected.Message);
            }


            var registerVM = formControl.DataContext as ApplicationUserViewModel;
            if (registerVM is null)
            {
                DialogService.ShowError(ErrorCatalog.Server.Unexpected.Message);
            }

            if (registerVM.IsValid)
            {
                var appUser = registerVM.ToModel();
                await _employeeService.CreateAsync(appUser);
                this.Content = _serviceProvider.GetRequiredService<NewEmployeeControl>();
                DialogService.ShowSuccess("✅ تم حفظ بيانات الموظف بنجاح.");
            }

        }

    }
}
