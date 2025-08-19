using App.BLL;
using App.BLL.Manager;
using App.Entities.Models;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MyApp.WPF.Mappers;
using MyApp.WPF.Services.Dialog;
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
        private readonly IBLayerManager _manager;
        private readonly IServiceProvider _serviceProvider;
        public EditCompaniesControl(IBLayerManager manager,IServiceProvider serviceProvider,CompanyViewModel companyViewModel)
        {
            InitializeComponent();

            _manager = manager;
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
                if (this.FormSection.Content is not CompanyFromControl { DataContext : CompanyViewModel companyVM})
                    return;

                if (!companyVM.IsValid)
                    return;

                var result = await _manager.CompanyService.GetByIdAsync(companyVM.Id);
                
                if(!result.State)
                {
                    DialogService.ShowError(result.Message);
                    return;
                }
                
                companyVM.ToModel(result.Data);
                
                var updateResult = await _manager.CompanyService.UpdateAsync(result.Data);
                
                if(!updateResult.State)
                {
                    DialogService.ShowError(updateResult.Message);
                    return;
                }

                DialogService.ShowSuccess("تم حفظ بيانات بنجاح.", "نجاح العملية");
                
                this.Content = ActivatorUtilities.CreateInstance<CompaniesControl>(_serviceProvider);
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }
        }
    }
}
