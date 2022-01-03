using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTrainningCSharp.ViewModel
{
    public class ViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string newParameter)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newParameter));
            }
        }
        protected Dictionary<string, List<string>> _errorsOnProperty = new Dictionary<string, List<string>>();
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

        protected void AddErrorOfPropertyInErrorsList(string property, string message)
        {
            if (!_errorsOnProperty.ContainsKey(property))
            {
                _errorsOnProperty.Add(property, new List<string>());
            }
            _errorsOnProperty[property].Add(message);
            OnErrorChanged(property);
        }

        protected void ClearErrorsOfProperty(string property)
        {
            if (_errorsOnProperty.ContainsKey(property))
            {
                _errorsOnProperty.Remove(property);
                OnErrorChanged(property);
            }
        }
        private void OnErrorChanged(string property)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(property));
        }

    }
}
