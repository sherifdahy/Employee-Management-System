using App.BLL;
using App.BLL.Manager;
using App.Entities.Models;
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
    /// <summary>
    /// Interaction logic for EmployeesControl.xaml
    /// </summary>
    public partial class EmployeesControl : UserControl
    {
        private readonly IBLayerManager _manager;
        private readonly IServiceProvider _serviceProvider;
        public EmployeesControl(IBLayerManager manager,IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _manager = manager;
            _serviceProvider = serviceProvider;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await UsePagination();
        }
        private async Task UsePagination(int userId = 0, string value = null)
        {
            try
            {
                EmployeesDataGrid.IsBusy = true;

                var pageSize = EmployeeDataPager.PageSize;
                var currentPage = EmployeeDataPager.PageIndex;

                var result = await _manager.EmployeeService.GetAllAsync(currentPage, pageSize, value);
                if(!result.State)
                {
                    DialogService.ShowError(result.Message);
                    return;
                }

                EmployeesDataGrid.ItemsSource = result.Data.Items;
                EmployeeDataPager.ItemCount = result.Data.TotalCount;
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }
            finally
            {
                EmployeesDataGrid.IsBusy = false;
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button { DataContext : ApplicationUser model})
                return;

            this.Content = ActivatorUtilities.CreateInstance<EditEmployeeControl>(_serviceProvider, model.ToViewModel(new()));
        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is not Button { DataContext: ApplicationUser model })
                    return;

                var result = await _manager.EmployeeService.DeleteAsync(model.Id);

                if (!result.State)
                {
                    DialogService.ShowError(result.Message);
                    return;
                }

                this.UserControl_Loaded(sender, e);
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }
        }
    }
}
