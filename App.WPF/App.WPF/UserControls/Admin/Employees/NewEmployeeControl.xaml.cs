using App.BLL;
using App.BLL.Manager;
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
        private readonly IBLayerManager _manager;
        private readonly IServiceProvider _serviceProvider;
        public NewEmployeeControl(IBLayerManager manager,IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _manager = manager;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BodyContent.Content = ActivatorUtilities.CreateInstance<FormEmployeeControl>(_serviceProvider,new ApplicationUserViewModel());
        }
        private async void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BodyContent.Content is not FormEmployeeControl { DataContext: ApplicationUserViewModel applicationUserViewModel })
                    return;

                if (!applicationUserViewModel.IsValid)
                {
                    DialogService.ShowWarning(ErrorCatalog.Validation.RequiredFieldMissing.Message);
                    return;
                }    

                var appUser = applicationUserViewModel.ToModel(new());

                var result = await _manager.EmployeeService.CreateAsync(appUser);
                if(!result.State)
                {
                    DialogService.ShowError(result.Message);
                    return;
                }    

                this.Content = _serviceProvider.GetRequiredService<NewEmployeeControl>();
                DialogService.ShowSuccess(" تم حفظ بيانات الموظف بنجاح.");
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }

        }

    }
}
