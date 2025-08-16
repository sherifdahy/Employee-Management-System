using App.BLL;
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
        private readonly IEmployeeService _employeeService;
        private readonly IServiceProvider _serviceProvider;
        public EmployeesControl(IEmployeeService employeeService,IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _employeeService = employeeService;
            _serviceProvider = serviceProvider;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await UsePagination();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ ما.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async Task UsePagination(int userId = 0, string value = null)
        {
            var pageSize = EmployeeDataPager.PageSize;
            var currentPage = EmployeeDataPager.PageIndex;
            var paginationResult = await _employeeService.GetAllAsync(currentPage, pageSize, value);
            EmployeesDataGrid.ItemsSource = paginationResult.Items;
            EmployeeDataPager.ItemCount = paginationResult.TotalCount;
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.DataContext is ApplicationUser model)
                {
                    this.Content = ActivatorUtilities.CreateInstance<EditEmployeeControl>(_serviceProvider, model.ToViewModel());
                }
            }
        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn) { 
                if(btn.DataContext is ApplicationUser model)
                {
                    var result = await _employeeService.DeleteAsync(model.Id);
                    if(!result.State)
                    {
                        DialogService.ShowError(result.Message);
                    }
                    else
                    {
                        this.UserControl_Loaded(sender, e);
                    }
                }
            }
        }
    }
}
