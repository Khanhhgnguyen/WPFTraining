using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using WPFTraining.Model;
using Prism.Mvvm;
using Prism.Commands;
using System.Windows.Input;
using System.Windows;

namespace WPFTraining.ViewModel
{
    public class OrderDetailViewModel : INotifyPropertyChanged
    {
        private OrderInfo dueDate;
        private ObservableCollection<OrderInfo> _dueDate;
        public ObservableCollection<OrderDetail> _order;

        public OrderInfo DueDate
        {
            get { return dueDate; }
            set
            {
                dueDate = value;
                OnPropertyChanged("DueDate");
            }
        }

        public ObservableCollection<OrderInfo> _DueDate
        {
            get { return _dueDate; }
            set
            {
                _dueDate = value;
                OnPropertyChanged("_DueDate");
            }
        }

        public ObservableCollection<OrderDetail> Order
        {
            get { return _order; }
            set
            {
                _order = value;
                OnPropertyChanged("Order");
            }
        }

        private OrderDetail _orderdetail;
        public OrderDetail OrderDetail
        {
            get { return _orderdetail; }
            set
            {
                _orderdetail = value;
                OnPropertyChanged("OrderDetail");
            }
        }
        public OrderDetailViewModel()
        {
            Order = new ObservableCollection<OrderDetail>(getOrderDetail());
        }

        private DelegateCommand<OrderDetail> deleteCommand;

        public DelegateCommand<OrderDetail> DeleteCommand
        {
            get { return deleteCommand = new DelegateCommand<OrderDetail>(ExecuteDeleteCommand); }
            set
            {
                deleteCommand = new DelegateCommand<OrderDetail>(ExecuteDeleteCommand);
                OnPropertyChanged("DeleteCommand");
            }
        }


        //public DelegateCommand<OrderDetail> DeleteCommand => deleteCommand ?? (deleteCommand = new DelegateCommand<OrderDetail>(ExecuteDeleteCommand));
        void ExecuteDeleteCommand(OrderDetail parameter)
        {
            Order.Remove(parameter);
        }

        private DelegateCommand<OrderDetail> selectCommand;

        //public DelegateCommand<OrderDetail> SelectCommand => selectCommand ?? (selectCommand = new DelegateCommand<OrderDetail>(ExecuteSelectCommand));

        public DelegateCommand<OrderDetail> SelectCommand
        {
            get { return selectCommand = new DelegateCommand<OrderDetail>(ExecuteSelectCommand); }
            set
            {
                selectCommand = new DelegateCommand<OrderDetail>(ExecuteSelectCommand);
                OnPropertyChanged("SelectCommand");
            }
        }
        void ExecuteSelectCommand(OrderDetail parameter)
        {
            string messageSeq = parameter.Seq.ToString() + "\n" + parameter.ItemCode + "\n" + parameter.Description + "\n" + parameter.FinalAmt.ToString();
            MessageBox.Show(messageSeq, "NotifyUsingICommand");
        }


        //private DelegateCommand<OrderDetail> saveCommand;

        //public DelegateCommand<OrderDetail> SaveCommand
        //{
        //    get { return saveCommand = new DelegateCommand<OrderDetail>(ExecuteSaveCommand); }
        //    set
        //    {
        //        saveCommand = new DelegateCommand<OrderDetail>(ExecuteSaveCommand);
        //        OnPropertyChanged("SaveCommand");
        //    }
        //}

        void ExecuteSaveCommand(OrderDetail parameter)
        {
            string dataShowed = parameter.Name + "\n" + parameter.Seq + "\n" + parameter.ItemCode;
            MessageBox.Show(dataShowed, "Data Current Showed");
            new RoutedUICommand ("ExecuteSaveCommand", "ExecuteSaveCommand", typeof(OrderDetailViewModel), new InputGestureCollection(){ new KeyGesture(Key.S, ModifierKeys.Alt) });
        }
        public static ObservableCollection<OrderDetail> getOrderDetail()
        {
            var orderDetail = new ObservableCollection<OrderDetail>();
            orderDetail.Add(new OrderDetail() { Name = "Luffy", Seq = 1, ItemCode = "STK000001", Description = "APPLE IPAD CASING - WHITE", UOM = "PC", UnitPrice = 100, Quantity = 30, Disc = 0.00, Tax = 1 });
            orderDetail.Add(new OrderDetail() { Name = "Luffy", Seq = 2, ItemCode = "STK000001", Description = "APPLE IPAD CASING - WHITE", UOM = "PC", UnitPrice = 90, Quantity = 30, Disc = 0.00, Tax = 1 });
            orderDetail.Add(new OrderDetail() { Name = "Luffy", Seq = 3, ItemCode = "STK000002", Description = "APPLE IPAD CASING - WHITE", UOM = "PC", UnitPrice = 160, Quantity = 30, Disc = 0.00, Tax = 1 });
            orderDetail.Add(new OrderDetail() { Name = "Luffy", Seq = 4, ItemCode = "STK000003", Description = "APPLE IPAD CASING - WHITE", UOM = "PC", UnitPrice = 10, Quantity = 30, Disc = 0.00, Tax = 1 });
            orderDetail.Add(new OrderDetail() { Name = "Khanh", Seq = 5, ItemCode = "STK000004", Description = "APPLE IPAD CASING - WHITE", UOM = "PC", UnitPrice = 260, Quantity = 30, Disc = 0.00, Tax = 1 });
            orderDetail.Add(new OrderDetail() { Name = "Khanh", Seq = 6, ItemCode = "STK000005", Description = "APPLE IPAD CASING - WHITE", UOM = "PC", UnitPrice = 30, Quantity = 30, Disc = 0.00, Tax = 1 });
            orderDetail.Add(new OrderDetail() { Name = "Khanh", Seq = 7, ItemCode = "STK000006", Description = "APPLE IPAD CASING - WHITE", UOM = "PC", UnitPrice = 5, Quantity = 30, Disc = 0.00, Tax = 1 });
            return orderDetail;
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
