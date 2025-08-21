using App.Entities.Models;
using MyApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.Mappers
{
    public static class TransactionItemMapper
    {
        #region ViewModel => Model
        public static TransactionItem ToModel(this TransactionItemViewModel viewModel,TransactionItem transactionItem)
        {
            if (viewModel is null || transactionItem is null)
                return null;

            transactionItem.Note = viewModel.Note;
            transactionItem.TransactionItemCategoryId = viewModel.TransactionItemCategoryId;
            transactionItem.Amount = viewModel.Amount;
            transactionItem.FileUrl = viewModel.FileUrl;
            transactionItem.CompanyId = viewModel.CompanyId;

            return transactionItem;
        }

        public static ICollection<TransactionItem> ToModel(this IEnumerable<TransactionItemViewModel> viewModels , ICollection<TransactionItem> transactionItems)
        {
            var transactionItemsDictionary = transactionItems.ToDictionary(x=>x.Id);

            foreach(var transactionItemVM in viewModels)
            {
                if(!transactionItemsDictionary.TryGetValue(transactionItemVM.Id,out TransactionItem transactionItem))
                {
                    // new
                    transactionItems.Add(transactionItemVM.ToModel(new TransactionItem()));
                }
                else
                {
                    // exist
                    transactionItemVM.ToModel(transactionItem);
                }
            }

            foreach(var transactionItem in transactionItems.ToList())
            {
                if(!viewModels.Any(x=>x.Id == transactionItem.Id))
                {
                    transactionItems.Remove(transactionItem);
                }
            }

            return transactionItems;
        }
        #endregion
    }
}
