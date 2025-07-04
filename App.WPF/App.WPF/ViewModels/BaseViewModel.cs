using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.ViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class BaseViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors => _errors.Any();

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        // طريقة محسّنة لتعيين القيم مع التحقق التلقائي
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            ValidateProperty(value, propertyName);
            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual void ValidateProperty(object value, string propertyName)
        {
            var validationContext = new ValidationContext(this) { MemberName = propertyName };
            var validationResults = new List<ValidationResult>();

            // مسح الأخطاء السابقة
            _errors.Remove(propertyName);

            // التحقق من الصحة
            if (!Validator.TryValidateProperty(value, validationContext, validationResults))
            {
                var errors = validationResults.Select(r => r.ErrorMessage).ToList();
                _errors[propertyName] = errors;
            }

            OnErrorsChanged(propertyName);
        }

        // التحقق من صحة كامل الكائن
        public virtual bool ValidateAll()
        {
            var validationContext = new ValidationContext(this);
            var validationResults = new List<ValidationResult>();

            _errors.Clear();

            if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
            {
                foreach (var validationResult in validationResults)
                {
                    foreach (var memberName in validationResult.MemberNames)
                    {
                        if (!_errors.ContainsKey(memberName))
                            _errors[memberName] = new List<string>();

                        _errors[memberName].Add(validationResult.ErrorMessage);
                    }
                }
            }

            // إشعار بتغيير الأخطاء لكل الخصائص
            foreach (var property in GetType().GetProperties())
            {
                OnErrorsChanged(property.Name);
            }

            return !HasErrors;
        }

        public virtual IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return _errors.SelectMany(e => e.Value);

            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : Enumerable.Empty<string>();
        }
    }

   
}
