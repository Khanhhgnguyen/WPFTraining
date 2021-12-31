using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFTraining.Model;

namespace WPFTraining.ViewModel
{
    public class ValidationViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public string Error { get { return null; } }

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public string this[string number]
        {
            get
            {
                string result = null;
                switch (number)
                {
                    case "OrderNo":
                        if (string.IsNullOrWhiteSpace(OrderNo))
                            result = "Order No. can not be empty";
                        else if (OrderNo.Length > 10)
                            result = "Order No. is required and Length <= 10 chars";
                        break;

                    case "CurrentCode":
                        if (string.IsNullOrWhiteSpace(CurrentCode))
                            result = "Current Code can be not empty";
                        else if (CurrentCode.Length != 3 || string.IsNullOrWhiteSpace(CurrentCode))
                            result = "Currency is required and Length = 3 chars";
                        break;
                    case "DateBegin":
                        if (DueDate < DateBegin)
                            result = "Due By must be greater than Date";
                        break;
                    case "DueDate":
                        if (DueDate < DateBegin)
                            result = "Due By must be greater than Date";
                        break;
                    case "ShippingDate":
                        if (ShippingDate < DateBegin)
                            result = "ShippingDate must be greater than Date";
                        break;
                }

                if (ErrorCollection.ContainsKey(number))
                {
                    ErrorCollection[number] = result;
                }
                else if (result != null)
                {
                    ErrorCollection.Add(number, result);
                }
                OnPropertyChanged("ErrorCollection");
                return result;
            }
        }
        private string orderNo;
        public string OrderNo
        {
            get { return orderNo; }
            set
            {
                orderNo = value;
                OnPropertyChanged("OrderNo");
            }
        }

        private string currentCode;
        public string CurrentCode
        {
            get { return currentCode; }
            set
            {
                currentCode = value;
                OnPropertyChanged("CurrentCode");
            }
        }

        private DateTime dateBegin;
        public DateTime DateBegin
        {
            get { return dateBegin; }
            set
            {
                dateBegin = value;
                OnPropertyChanged("DateBegin");
            }
        }

        private DateTime dueDate;
        public DateTime DueDate
        {
            get { return dueDate; }
            set
            {
                dueDate = value;
                OnPropertyChanged("DateBegin");
            }
        }

        private DateTime shippingDate;
        public DateTime ShippingDate
        {
            get { return shippingDate; }
            set
            {
                shippingDate = value;
                OnPropertyChanged("ShippingDate");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newParameter)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newParameter));
            }
        }

        private DelegateCommand<OrderDetail> saveCommand;

        public DelegateCommand<OrderDetail> SaveCommand
        {
            get { return saveCommand = new DelegateCommand<OrderDetail>(ExecuteSaveCommand); }
            set
            {
                saveCommand = new DelegateCommand<OrderDetail>(ExecuteSaveCommand);
                OnPropertyChanged("SaveCommand");
            }
        }
        void ExecuteSaveCommand(OrderDetail parameter)
        {
            string messageSeq = DueDate.ToString() + "\n" + ShippingDate.ToString() + "\n" + parameter.Seq.ToString();
            MessageBox.Show(messageSeq, "SaveCommand");
        }



        //public List<DateTime> BindDatesToComboBox()
        //{
        //    List<DateTime> datesOfMonth = new List<DateTime>();
        //    datesOfMonth.AddRange(Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
        //            .Select(day => new DateTime(DateTime.Now.Year, DateTime.Now.Month, day))
        //            .ToList()); // Load dates into a list

        //    return datesOfMonth;
        //}
    }
}
