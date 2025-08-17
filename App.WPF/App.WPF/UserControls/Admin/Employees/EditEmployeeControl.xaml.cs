using App.BLL;
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
        private readonly IEmployeeService _employeeService;
        public EditEmployeeControl(IServiceProvider serviceProvider,IEmployeeService employeeService,ApplicationUserViewModel applicationUserViewModel)
        {
            InitializeComponent();
            this.DataContext = applicationUserViewModel;
            _serviceProvider = serviceProvider;
            _employeeService = employeeService;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BodyContent.Content = ActivatorUtilities.CreateInstance<FormEmployeeControl>(_serviceProvider,this.DataContext);
        }

        private async void EditEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as ApplicationUserViewModel;
            if(vm is null)
            {
                DialogService.ShowError(ErrorCatalog.Server.Unexpected.Message);
                return;
            }

            var result = await _employeeService.GetByIdAsync(vm.Id);
            if(!result.State)
            { 
                DialogService.ShowError(result.Message);
                return;
            }

            vm.ToModel(result.Data);
            await _employeeService.UpdateAsync(result.Data);

            this.Content = ActivatorUtilities.CreateInstance<EmployeesControl>(_serviceProvider);
        }

    }
}
