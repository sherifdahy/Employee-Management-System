using App.BLL;
using App.Entities.Models;
using AutoMapper;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;
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
                var companyVM = this.DataContext as CompanyViewModel;

                if(companyVM.IsValid)
                {
                    await _companyService.CreateAsync(_mapper.Map<Company>(companyVM));
                    MessageBox.Show(
                    "✅ تم حفظ بيانات الشركة بنجاح.",
                    "نجاح العملية",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information,
                    MessageBoxResult.OK);

                    // to reset form
                    this.DataContext = new CompanyViewModel();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"حدث خطأ ما",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void AddCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var window = _serviceProvider.GetRequiredService<NewOwnerWindow>();
                if (window.ShowDialog() == true)
                {
                    OwnersDataGrid.Items.AddNewItem(window.DataContext as OwnerViewModel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ ما", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        } 

        private void AddEmailBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NewEmailWindow window = _serviceProvider.GetRequiredService<NewEmailWindow>();
                if (window.ShowDialog() == true)
                {
                    EmailsDataGrid.Items.AddNewItem(window.DataContext as EmailViewModel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "حدث خطأ ما", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #region Helper
        private void TaxRegistrationNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);
        }
        private bool IsTextNumeric(string text)
        {
            return !Regex.IsMatch(text, @"^\d+$");
        }

        private void TaxRegistrationNumber_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            string text = (string)e.DataObject.GetData(typeof(string));
            if (IsTextNumeric(text))
            {
                e.CancelCommand();
            }
        }
        #endregion

        
    }
}