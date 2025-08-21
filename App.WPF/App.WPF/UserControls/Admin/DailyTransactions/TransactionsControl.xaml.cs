using App.BLL.Manager;
using MyApp.WPF.Services.Dialog;
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

namespace MyApp.WPF.UserControls.Admin.DailyTransactions
{
    /// <summary>
    /// Interaction logic for TransactionsControl.xaml
    /// </summary>
    public partial class TransactionsControl : UserControl
    {
        private readonly IBLayerManager _manager;
        public TransactionsControl(IBLayerManager manager)
        {
            InitializeComponent();

            _manager = manager;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ReloadTransactionsData();
        }

        private void TransactionDataPager_PageIndexChanged(object sender, Telerik.Windows.Controls.PageIndexChangedEventArgs e)
        {
            ReloadTransactionsData();
        }

        private async void ReloadTransactionsData()
        {
            try
            {
                TransactionsDataGrid.IsBusy = true;
                if (_manager is null)
                    return;
                int pageSize = TransactionDataPager.PageSize;
                int pageIndex = TransactionDataPager.PageIndex;
                string value = TransactionSearchBox.Text;

                var result = await _manager?.TransactionService?.GetAllAsync(pageIndex, pageSize, value);
                if (!result.State)
                {
                    DialogService.ShowError(result.Message);
                    return;
                }
                TransactionDataPager.ItemCount = result.Data.TotalCount;
                TransactionsDataGrid.ItemsSource = result.Data.Items;
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }
            finally
            {
                TransactionsDataGrid.IsBusy = false;
            }
        }

        private void TransactionSearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                ReloadTransactionsData();
            }
        }

        private void TransactionSearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ReloadTransactionsData();
        }
    }
}
