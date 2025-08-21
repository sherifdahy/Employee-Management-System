using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.ViewModels
{
    public class TransactionItemViewModel : BaseViewModel
    {
        private string _note;
        private decimal _amount;
        private string _fileUrl;
        private int _transactionItemCategoryId;
        private int _companyId;


        public int Id { get; set; }
        public string Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }

        [Required(ErrorMessage = "المبلغ مطلوب")]
        [Range(0.01, double.MaxValue, ErrorMessage = "المبلغ يجب أن يكون أكبر من صفر")]
        public decimal Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }

        public string FileUrl
        {
            get => _fileUrl;
            set => SetProperty(ref _fileUrl, value);
        }

        [Required(ErrorMessage = "الفئة مطلوبة")]
        public int TransactionItemCategoryId
        {
            get => _transactionItemCategoryId;
            set => SetProperty(ref _transactionItemCategoryId, value);
        }

        [Required(ErrorMessage = "الشركة مطلوبة")]
        public int CompanyId
        {
            get => _companyId;
            set => SetProperty(ref _companyId, value);
        }

    }
}
