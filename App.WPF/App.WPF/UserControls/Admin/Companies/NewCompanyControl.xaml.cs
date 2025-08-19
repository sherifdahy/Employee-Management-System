using App.BLL;
using App.BLL.Manager;
using App.Entities.Models;
using AutoMapper;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MyApp.WPF.Mappers;
using MyApp.WPF.Services.Dialog;
using MyApp.WPF.ViewModels;
using MyApp.WPF.Windows;
using MyApp.WPF.Windows.Admin;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyApp.WPF.UserControls.Admin.Companies
{
    public partial class NewCompanyControl : UserControl
    {
        private readonly IBLayerManager _manager;
        private readonly IServiceProvider _serviceProvider;
        public NewCompanyControl(IBLayerManager manager,IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _manager = manager;
            this.FormControl.DataContext = new CompanyViewModel();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.FormControl.Content = ActivatorUtilities.CreateInstance<CompanyFromControl>(_serviceProvider);
        }
        private async void CreateCompanyBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FormControl.Content is not CompanyFromControl { DataContext : CompanyViewModel companyVM })
                    return;

                if (!companyVM.IsValid)
                {
                    DialogService.ShowError("من فضلك اكمل البيانات بشكل صحيح.");
                    return;
                }

                var result = await _manager.CompanyService.CreateAsync(companyVM.ToModel(new Company()));

                if (!result.State)
                {
                    DialogService.ShowError(result.Message);
                    return;
                }

                DialogService.ShowSuccess("تم حفظ بيانات الشركة بنجاح.");

                if (FormControl.Content is CompanyFromControl control)
                {
                    control.DataContext = new CompanyViewModel();
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowError("حدث خطأ غير متوقع: " + ex.Message);
            }
        }

    }
}