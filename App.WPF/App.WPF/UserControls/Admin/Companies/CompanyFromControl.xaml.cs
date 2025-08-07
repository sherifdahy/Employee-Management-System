using App.BLL;
using Microsoft.Extensions.DependencyInjection;
using MyApp.WPF.Mappers;
using MyApp.WPF.Services.Dialog;
using MyApp.WPF.ViewModels;
using MyApp.WPF.Windows.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace MyApp.WPF.UserControls.Admin.Companies
{
    /// <summary>
    /// Interaction logic for CompanyFromControl.xaml
    /// </summary>
    public partial class CompanyFromControl : UserControl
    {
        private readonly ICompanyService _companyService;
        private readonly IServiceProvider _serviceProvider;
        public CompanyFromControl(ICompanyService companyService, IServiceProvider serviceProvider, CompanyViewModel companyViewModel)
        {
            InitializeComponent();

            this._companyService = companyService;
            this._serviceProvider = serviceProvider;
            this.DataContext = companyViewModel;
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

        private void DeleteEmailBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var companyVM = this.DataContext as CompanyViewModel;
                var button = sender as Button;
                var emailVM = button?.DataContext as EmailViewModel;
                if (companyVM is not null && emailVM is not null) 
                {
                    companyVM.Emails.Remove(emailVM);
                }
            }
            catch (Exception ex) 
            {
                DialogService.ShowError(ex.Message);
            }
        }

        private void EditEmailBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = sender as Button;
                var originalVM = btn?.DataContext as EmailViewModel;
                var companyVM = this.DataContext as CompanyViewModel;
                if(originalVM is not null && companyVM is not null)
                {
                    var window = ActivatorUtilities.CreateInstance<NewEmailWindow>(_serviceProvider, originalVM.Clone());
                    var result = window.ShowDialog();
                    if (result == true) 
                    {
                        var updatedEmailVM = window.DataContext as EmailViewModel;
                        updatedEmailVM.ToViewModel(originalVM);
                    }
                }
            }
            catch (Exception ex) 
            {
                DialogService.ShowError(ex.Message);
            }
        }

        private void DeleteOwnerBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var companyVM = this.DataContext as CompanyViewModel;
                var button = sender as Button;
                var ownerVM = button?.DataContext as OwnerViewModel;
                if (companyVM is not null && ownerVM is not null)
                {
                    companyVM.Owners.Remove(ownerVM);
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }
        }

        private void EditOwnerBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
