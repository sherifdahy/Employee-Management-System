using App.BLL.Manager;
using App.Entities.Models;
using MediaFoundation;
using Microsoft.Extensions.DependencyInjection;
using MyApp.WPF.Mappers;
using MyApp.WPF.Services.Dialog;
using MyApp.WPF.Services.State;
using MyApp.WPF.ViewModels;
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
    /// Interaction logic for NewTransactionControl.xaml
    /// </summary>
    public partial class NewTransactionControl : UserControl
    {
        private readonly IBLayerManager _manager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IStateService _stateService;

        public NewTransactionControl(IStateService stateService,IBLayerManager manager,IServiceProvider serviceProvider)
        {
            InitializeComponent();


            _stateService = stateService;
            _serviceProvider = serviceProvider;
            _manager = manager;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Form.Content = ActivatorUtilities.CreateInstance<FormTransactionControl>(_serviceProvider,new TransactionViewModel());
        }

        private async void NewTransactionBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.Form.Content is not FormTransactionControl { DataContext : TransactionViewModel transactionViewModel })
                    return;

                transactionViewModel.ApplicationUserId = _stateService.UserId;

                if (!transactionViewModel.ValidateAll())
                {
                    DialogService.ShowError("بعد الحقول تحتوي علي اخطاء");
                    return;
                }

                var transaction = transactionViewModel.ToModel(new DailyTransaction());
                var result = await _manager.TransactionService.CreateAsync(transaction);

                if(!result.State)
                {
                    DialogService.ShowError(result.Message);
                    return;
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }
        }
    }
}
