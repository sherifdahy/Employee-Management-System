using App.BLL;
using App.BLL.Manager;
using App.Entities.Models;
using Interfaces;
using MyApp.WPF.Services.Dialog;
using MyApp.WPF.ViewModels;
using Repository;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace MyApp.WPF.UserControls.Admin.Employees
{
    public partial class EmployeeAccessControl : UserControl
    {
        private readonly IBLayerManager _manager;
        public EmployeeAccessControl(IBLayerManager manager)
        {
            InitializeComponent();
            _manager = manager;
            Loaded += UserControl_Loaded;
            CompaniesDataPager.PageIndexChanged += CompaniesDataPager_PageIndexChanged;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await UsePagination(CompaniesDataGrid);
            await UsePagination(EmployeesDataGrid);
        }

        private async void CompanySearchTxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender is not RadWatermarkTextBox { Text: string value })
                return;

            await UsePagination(CompaniesDataGrid);
        }

        private void CompaniesDataGrid_RowActivated(object sender, Telerik.Windows.Controls.GridView.RowEventArgs e)
        {
            if (e.Row.Item is not Company company)
                return;

            if (EmployeesDataGrid.SelectedItem is not ApplicationUser employee)
            {
                DialogService.ShowWarning("من فضلك اختر موظفًا قبل تنفيذ هذه العملية.");
                return;
            }

            if (employee.Companies.Any(x => x.Id == company.Id))
            {
                DialogService.ShowWarning("هذه الشركة مضافة بالفعل للموظف.");
                return;
            }

            employee.Companies.Add(company);
            CompaniesAccessDataGrid.ItemsSource = null;
            CompaniesAccessDataGrid.ItemsSource = employee.Companies;
        }


        private async void EmployeesDataGrid_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            var employee = EmployeesDataGrid.SelectedItem as ApplicationUser;
            if (employee != null)
            {
                await UsePagination(CompaniesAccessDataGrid);
            }
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var employees = EmployeesDataGrid.ItemsSource as List<ApplicationUser>;
                if (employees != null)
                {
                    await _manager.EmployeeService.UpdateRangeAsync(employees);
                    DialogService.ShowSuccess("تم الحفظ بنجاح", "تمت العملية");
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message, "حدث خطأ ما.");
            }
        }

        private async void CompaniesAccessSeachTxt_KeyUp(object sender, KeyEventArgs e)
        {
            var employee = EmployeesDataGrid.SelectedItem as ApplicationUser;
            if ( employee != null)
            {
                await UsePagination(CompaniesAccessDataGrid);
            }
        }

        private async void CompaniesDataPager_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            await UsePagination(CompaniesDataGrid);
        }

        private async void EmployeesDataPager_PageIndexChanging(object sender, PageIndexChangingEventArgs e)
        {
            await UsePagination(CompaniesDataGrid);
        }

        private async void CompaniesAccessDataPager_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            await UsePagination(CompaniesAccessDataGrid);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button { DataContext : Company company })
                return;

            if (EmployeesDataGrid.SelectedItem is not ApplicationUser applicationUser)
                return;

            applicationUser.Companies.Remove(company);

            await UsePagination(CompaniesAccessDataGrid);
        }

        private async Task UsePagination(RadGridView table)
        {
            try
            {
                switch (table.Name)
                {
                    case nameof(CompaniesAccessDataGrid):
                        {
                            if (EmployeesDataGrid?.SelectedItem is not ApplicationUser applicationUser)
                                return;

                            if (CompaniesAccessSeachTxt is not RadWatermarkTextBox { Text: string value })
                                return;

                            int currentPage = CompaniesAccessDataPager.PageIndex;
                            int pageSize = CompaniesAccessDataPager.PageSize;
                            int skip = pageSize * currentPage;

                            Func<Company, bool> filter = x =>
                                string.IsNullOrEmpty(value)
                                || (!string.IsNullOrEmpty(x.Name) && x.Name.Contains(value, StringComparison.OrdinalIgnoreCase))
                                || (!string.IsNullOrEmpty(x.TaxRegistrationNumber) && x.TaxRegistrationNumber.Contains(value));

                            var filteredCompanies = applicationUser.Companies.Where(filter).ToList();

                            var pagedCompanies = filteredCompanies
                                                   .Skip(skip)
                                                   .Take(pageSize)
                                                   .ToList();

                            CompaniesAccessDataGrid.ItemsSource = pagedCompanies;
                            CompaniesAccessDataPager.ItemCount = filteredCompanies.Count;
                            break;
                        }


                    case nameof(EmployeesDataGrid):
                        {
                            int currentPage = EmployeesDataPager.PageIndex;
                            int pageSize = EmployeesDataPager.PageSize;

                            var result = await _manager.EmployeeService.GetAllAsync(currentPage, pageSize, null);

                            if (!result.State)
                            {
                                DialogService.ShowError(result.Message);
                                return;
                            }

                            EmployeesDataGrid.ItemsSource = result.Data.Items;
                            EmployeesDataPager.ItemCount = result.Data.TotalCount;
                            break;
                        }

                    case nameof(CompaniesDataGrid):
                        {
                            if (CompanySearchTxt is not RadWatermarkTextBox { Text: string value })
                                return;


                            int currentPage = CompaniesDataPager.PageIndex;
                            int pageSize = CompaniesDataPager.PageSize;

                            var result = await _manager.CompanyService.GetAllAsync(currentPage, pageSize, default, value);

                            if (!result.State)
                            {
                                DialogService.ShowError(result.Message);
                                return;
                            }

                            CompaniesDataGrid.ItemsSource = result.Data.Items;
                            CompaniesDataPager.ItemCount = result.Data.TotalCount;
                            break;
                        }

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }
        }

        
    }
}
