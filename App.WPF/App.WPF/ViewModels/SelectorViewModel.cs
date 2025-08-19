using App.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.ViewModels
{
    public class SelectorViewModel : BaseViewModel
    {
        #region PrivateFields
        string _value;
        SelectorType _selectorType;
        ContentType _contentType;
        #endregion

        public Guid Guid { get; set; }

        [Required]
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                SetProperty(ref _value, value);
            }
        }
        [Required]
        [Range(1, int.MaxValue)]
        public SelectorType SelectorType
        {
            get
            {
                return _selectorType;
            }
            set
            {
                SetProperty(ref _selectorType, value);
            }
        }
        [Required]
        [Range(1, int.MaxValue)]
        public ContentType ContentType { 
            get {
                return _contentType;
            } 
            set{
                SetProperty(ref _contentType, value);
            }}
        public bool IsValid => ValidateAll();
    }
}
