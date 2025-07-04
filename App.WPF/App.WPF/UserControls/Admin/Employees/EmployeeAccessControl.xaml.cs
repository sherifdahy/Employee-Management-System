using App.BLL;
using App.Entities.Models;
using Interfaces;
using MyApp.WPF.ViewModels;
using Repository;
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
using Telerik.Windows.Controls;

namespace MyApp.WPF.UserControls.Admin.Employees
{
    public partial class EmployeeAccessControl : UserControl
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICompanyService _companyService;
        public EmployeeAccessControl(IEmployeeService employeeService, ICompanyService companyService)
        {
            InitializeComponent();
            _employeeService = employeeService;
            _companyService = companyService;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            try
            {
                var employees = await _employeeService.GetAllAsync();
                var companies = await _companyService.GetAllAsync();
                CompaniesDataGrid.Items.AddRange(companies);
                EmployeesDataGrid.Items.AddRange(employees);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ ما.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void CompanySearchTxt_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                RadWatermarkTextBox radWatermarkTextBox = sender as RadWatermarkTextBox;
                var companies = await _companyService.SearchAsync(radWatermarkTextBox.Text);
                CompaniesDataGrid.Items.AddRange(companies);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ ما.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CompaniesDataGrid_RowActivated(object sender, Telerik.Windows.Controls.GridView.RowEventArgs e)
        {
            try
            {
                var company = e.Row.Item as Company;
                var employee = EmployeesDataGrid.SelectedItem as ApplicationUser;
                if (employee != null)
                {
                    bool result = employee.Companies.Contains(company);
                    if (!result)
                    {
                        employee.Companies.Add(company);
                        CompaniesAccessDataGrid.Items.Clear();
                        CompaniesAccessDataGrid.Items.AddRange(employee.Companies);
                        return;
                    }
                    MessageBox.Show("هذه الشركة مضافة بالفعل للموظف.", "فشل العملية", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                MessageBox.Show("من فضلك اختر موظفًا قبل تنفيذ هذه العملية.", "فشل العملية", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ ما.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

       
        private void EmployeesDataGrid_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            try
            {
                var employee = EmployeesDataGrid.SelectedItem as ApplicationUser;
                if (employee != null)
                {
                    CompaniesAccessDataGrid.Items.Clear();
                    CompaniesAccessDataGrid.Items.AddRange(employee.Companies);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ ما.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var employees = EmployeesDataGrid.ItemsSource as List<ApplicationUser>;
                if (employees != null)
                {
                    _employeeService.UpdateRange(employees);
                    MessageBox.Show("تم الحفظ بنجاح", "تمت العمليه ", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ ما.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CompaniesAccessSeachTxt_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                var textBox = sender as RadWatermarkTextBox;
                var employee = EmployeesDataGrid.SelectedItem as ApplicationUser;
                if (textBox != null)
                {
                    if (employee != null)
                    {
                        //CompaniesAccessDataGrid.Items.Clear();
                        //var filteredCompanies = employee.Companies.Where(x => string.IsNullOrEmpty(textBox.Text) ? true : x.Name.Contains(textBox.Text) || x.TaxRegistrationNumber.Contains(textBox.Text));
                        //CompaniesAccessDataGrid.Items.AddRange(filteredCompanies);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ ما.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
