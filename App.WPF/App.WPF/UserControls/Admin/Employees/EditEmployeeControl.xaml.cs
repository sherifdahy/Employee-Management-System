using App.BLL;
using App.BLL.Manager;
using App.Entities;
using App.Entities.Models;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyApp.WPF.UserControls.Admin.Employees
{
    /// <summary>
    /// Interaction logic for EditEmployeeControl.xaml
    /// </summary>
    public partial class EditEmployeeControl : UserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IBLayerManager _manager;
        public EditEmployeeControl(IServiceProvider serviceProvider,IBLayerManager manager,ApplicationUserViewModel applicationUserViewModel)
        {
            InitializeComponent();
            this.DataContext = applicationUserViewModel;
            _serviceProvider = serviceProvider;
            _manager = manager;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BodyContent.Content = ActivatorUtilities.CreateInstance<FormEmployeeControl>(_serviceProvider,this.DataContext);
        }

        private async void EditEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataContext is not ApplicationUserViewModel applicationUserViewModel)
                    return;

                if(!applicationUserViewModel.IsValid)
                {
                    DialogService.ShowWarning(ErrorCatalog.Validation.RequiredFieldMissing.Message);
                    return;
                }

                var result = await _manager.EmployeeService.GetByIdAsync(applicationUserViewModel.Id);
                if (!result.State)
                {
                    DialogService.ShowError(result.Message);
                    return;
                }

                applicationUserViewModel.ToModel(result.Data);
                var updateResult = await _manager.EmployeeService.UpdateAsync(result.Data);

                if(!updateResult.State)
                {
                    DialogService.ShowError(updateResult.Message);
                    return;
                }

                this.Content = ActivatorUtilities.CreateInstance<EmployeesControl>(_serviceProvider);
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }
        }

    }
}
