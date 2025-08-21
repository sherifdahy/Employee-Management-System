using App.Entities.Enums;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MyApp.WPF.ViewModels
{
    public class TransactionViewModel : BaseViewModel
    {
        private int _id;
        private TransactionState _state;
        private DateTime _transactionDate = DateTime.Now;
        private ObservableCollection<TransactionItemViewModel> _transactionItems = new ObservableCollection<TransactionItemViewModel>();

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }


        [Required(ErrorMessage = "تاريخ المعاملة مطلوب")]
        public DateTime TransactionDate
        {
            get => _transactionDate;
            set => SetProperty(ref _transactionDate, value);
        }

        public TransactionState State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        public ObservableCollection<TransactionItemViewModel> TransactionItems
        {
            get => _transactionItems;
            set
            {
                _transactionItems = value;
                OnPropertyChanged(nameof(TransactionItems));
            }
        }

        public int ApplicationUserId { get; set; }
    }
}
