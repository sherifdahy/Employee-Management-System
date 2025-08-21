using App.BLL.Manager;
using MyApp.WPF.Services.State;
using System.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using MyApp.WPF.Services.Dialog;
using App.Entities.Models;
using System.Collections.Generic;
using MyApp.WPF.ViewModels;

namespace MyApp.WPF.UserControls.Admin.DailyTransactions
{
    /// <summary>
    /// Interaction logic for FormTransactionControl.xaml
    /// </summary>
    public partial class FormTransactionControl : UserControl
    {
        private readonly IBLayerManager _manager;
        private readonly IStateService _stateService;

        public FormTransactionControl(IBLayerManager manager, IStateService stateService,TransactionViewModel transactionViewModel)
        {
            InitializeComponent();

            DataContext = transactionViewModel;
            _manager = manager;
            _stateService = stateService;

        }
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await InitCompanyComboBox();
        }
        public async Task InitCompanyComboBox()
        {
            try
            {
                var resultCategories = await _manager.TransactionItemCategoryService.GetAllAsync(0,int.MaxValue,null);
                var resultCompanies = await _manager.CompanyService.GetAllAsync();
                cmbCompany.ItemsSource = resultCompanies.Data;
                cmbCategory.ItemsSource = resultCategories.Data;
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }
        }

        private void cmbCompany_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

            //if (sender is not RadComboBox radComboBox)
            //    return;
            
            //radComboBox.ItemsSource = _companies.Where(x=>x.Name.Contains(radComboBox.Text));
        }


        private void btnAddToList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = new TransactionItemViewModel()
                {
                    Amount = Convert.ToDecimal(numAmount?.Value ?? 0),
                    Note = txtNotes?.Text,
                    FileUrl = txtFilePath?.Text,
                    TransactionItemCategoryId = cmbCategory?.SelectedValue is int catId ? catId : 0,
                    CompanyId = (int)cmbCompany?.SelectedValue,
                };

                if (!item.ValidateAll())
                {
                    DialogService.ShowError("❌ تأكد من إدخال كل البيانات المطلوبة");
                    return;
                }

                if (DataContext is not TransactionViewModel viewModel)
                    return;

                viewModel?.TransactionItems.Add(item);

                ClearForm();
            }
            catch (Exception ex)
            {
                DialogService.ShowError($"حصل خطأ أثناء الإضافة: {ex.Message}");
            }
        }

        private void ClearForm()
        {
            numAmount.Value = null;
            txtNotes.Text = string.Empty;
            txtFilePath.Text = string.Empty;
            cmbCategory.SelectedIndex = -1;
            cmbCompany.SelectedIndex = -1;
            cmbCategory.SelectedIndex = -1;
        }

    }
}
