using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTrainningCSharp.Validators
{
    public class Validators : IDataErrorInfo, INotifyDataErrorInfo, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string newParameter)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newParameter));
            }
        }

        public string this[string columnName] => throw new NotImplementedException();

        public string Error => throw new NotImplementedException();

        private void ValidateProperty<T>(T value, string name)
        {
            Validator.ValidateProperty(value, new ValidationContext(this, null, null)
                {
                MemberName = name
            });
        }
        public Dictionary<string, List<string>> _errorsOnProperty = new Dictionary<string, List<string>>(); //contains all errors of specific property
        public bool HasErrors
        {
            get
            {
                try
                {
                    List<string> errors = _errorsOnProperty.Values.FirstOrDefault(error => error.Count > 0);
                    if (errors != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return true;
                }
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;


        public IEnumerable GetErrors(string propertyName)
        {
            if (propertyName != null)
            {
                _errorsOnProperty.TryGetValue(propertyName, out List<string> errors);
                return errors;
            }
            else
            {
                return null;
            }
        }

        public void AddErrorOfPropertyInErrorsList(string property, string message)
        {
            if (!_errorsOnProperty.ContainsKey(property))
            {
                _errorsOnProperty.Add(property, new List<string>());
            }// end if property does not exist in errors list
            _errorsOnProperty[property].Add(message);
            OnErrorChanged(property);
        }

        public void ClearErrorsOfProperty(string property)
        {
            if (_errorsOnProperty.ContainsKey(property))
            {
                _errorsOnProperty.Remove(property);
                OnErrorChanged(property);
            }//Delete All Errors of that property before check to prevent duplicating error 
        }
        private void OnErrorChanged(string property)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(property));
        }
    }
}
