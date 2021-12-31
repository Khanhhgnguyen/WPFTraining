using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTraining.Model
{
    public abstract class OrderInfo : INotifyPropertyChanged
    {
        private DateTime dueDate;
        private DateTime shipDate;

        public DateTime DueDate
        {
            get { return dueDate; }
            set
            {
                dueDate = value;
                OnPropertyChanged("DueDate");
            }
        }

        public DateTime ShipDate
        {
            get { return shipDate; }
            set
            {
                shipDate = value;
                OnPropertyChanged("ShipDate");
            }
        }
        public OrderInfo()
        {

        }
        public OrderInfo(DateTime dueDate, DateTime shipDate)
        {
            this.dueDate = dueDate;
            this.shipDate = shipDate;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newParameter)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newParameter));
            }
        }
    }
}
