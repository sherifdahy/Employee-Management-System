using App.BLL;
using App.Entities.Models;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MyApp.WPF.Mappers;
using MyApp.WPF.UserControls.Admin.Employees;
using MyApp.WPF.UserControls.Employee.Companies;
using MyApp.WPF.ViewModels;
using MyApp.WPF.Windows.Admin;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace MyApp.WPF.UserControls.Admin.Companies
{
    /// <summary>
    /// Interaction logic for EditCompaniesControl.xaml
    /// </summary>
    public partial class EditCompaniesControl : UserControl
    {
        private readonly ICompanyService _companyService;
        private readonly IServiceProvider _serviceProvider;
        public EditCompaniesControl(ICompanyService companyService,IServiceProvider serviceProvider,CompanyViewModel companyViewModel)
        {
            InitializeComponent();

            this._companyService = companyService;
            this._serviceProvider = serviceProvider;
            this.FormSection.DataContext = companyViewModel;
            
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.FormSection.Content = ActivatorUtilities.CreateInstance<CompanyFromControl>(_serviceProvider);
        }
        private async void EditCompanyBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var formControl = this.FormSection.Content as CompanyFromControl;
                var companyVM = formControl.DataContext as CompanyViewModel;
                if (companyVM.IsValid)
                {
                    var result = await _companyService.GetByIdAsync(companyVM.Id);
                    if (result != null) 
                    {
                        companyVM.ToModel(result.Data);
                        await _companyService.UpdateAsync(result.Data);
                        // to reset form
                        MessageBox.Show("✅ تم حفظ بيانات بنجاح.","نجاح العملية",MessageBoxButton.OK,MessageBoxImage.Information,MessageBoxResult.OK);
                        this.Content = ActivatorUtilities.CreateInstance<CompaniesControl>(_serviceProvider);
                    }
                    else
                    {
                        throw new Exception(result.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ ما", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
    }
}
