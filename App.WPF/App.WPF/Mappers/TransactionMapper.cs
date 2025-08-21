using App.Entities.Models;
using MyApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.Mappers
{
    public static class TransactionMapper
    {
        public static DailyTransaction ToModel(this TransactionViewModel viewModel,DailyTransaction transaction)
        {
            transaction.State = viewModel.State;
            transaction.TransactionDate = viewModel.TransactionDate;
            transaction.ApplicationUserId = viewModel.ApplicationUserId;
            viewModel.TransactionItems.ToModel(transaction.TransactionItems);
            return transaction;
        }
    }
}
