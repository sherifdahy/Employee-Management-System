using App.BLL;
using App.Entities.Models;
using AutoMapper;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;
        private readonly ICompanyService _companyService;
        public NewCompanyControl(ICompanyService companyService,IMapper mapper,IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _mapper = mapper;
            _serviceProvider = serviceProvider;
            _companyService = companyService;
        }

        private async void CreateCompanyBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var companyVM = this.FormControl.DataContext as CompanyViewModel;

                if (companyVM.IsValid)
                {
                    var result = await _companyService.CreateAsync(_mapper.Map<Company>(companyVM));

                    if (result.State)
                    {
                        DialogService.ShowSuccess("✅ تم حفظ بيانات الشركة بنجاح.");
                        var control = this.FormControl.Content as CompanyFromControl;
                        control.DataContext = new CompanyViewModel();
                    }
                    else
                    {
                        DialogService.ShowWarning(result.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.FormControl.Content = ActivatorUtilities.CreateInstance<CompanyFromControl>(_serviceProvider,this.DataContext);
        }
    }
}