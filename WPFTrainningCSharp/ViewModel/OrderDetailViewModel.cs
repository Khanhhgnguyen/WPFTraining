using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFTrainningCSharp.Command;
using WPFTrainningCSharp.Model;
using WPFTrainningCSharp.Service;

namespace WPFTrainningCSharp.ViewModel
{
    public class OrderDetailViewModel : INotifyPropertyChanged
    {
        private OrderInfo dueDate;
        private ObservableCollection<OrderInfo> _dueDate;
        private ObservableCollection<OrderDetail> _order;
        private ICommand deleteCommand;
        private OrderDetail orderdetail;
        private ICommand selectCommand;

        public OrderDetailViewModel()
        {
            Order = new ObservableCollection<OrderDetail>(getOrderDetail());
        }

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


        public OrderDetail OrderDetail
        {
            get { return orderdetail; }
            set
            {
                orderdetail = value;
                OnPropertyChanged("OrderDetail");
            }
        }


        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new ExcuteCommand(DeletedCommand, CanDeleteCommand);
                }
                return deleteCommand;
            }
        }
        public bool CanDeleteCommand(object parameter)
        {
            return true;
        }


        public ICommand SelectCommand
        {
            get
            {
                if (selectCommand == null)
                {
                    selectCommand = new ExcuteCommand(ShowSelectCommand, CanSelectCommand);
                }
                return selectCommand;
            }
        }


        public void DeletedCommand(object parameter)
        {
            Order.Remove(orderdetail);
            OnPropertyChanged("Order");
        }

        public bool CanSelectCommand(object parameter)
        {
            if (OrderDetail != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ShowSelectCommand(object parameter)
        {
            MessageBox.Show(orderdetail.ToString());
        }

        void ExecuteSaveCommand(OrderDetail parameter)
        {
            string dataShowed = parameter.Name + "\n" + parameter.Seq + "\n" + parameter.ItemCode;
            MessageBox.Show(dataShowed, "Data Current Showed");
            new RoutedUICommand("ExecuteSaveCommand", "ExecuteSaveCommand", typeof(OrderDetailViewModel), new InputGestureCollection() { new KeyGesture(Key.S, ModifierKeys.Alt) });
        }
        public static ObservableCollection<OrderDetail> getOrderDetail()
        {

            var mockOrderDetail = new Mock<OrderDetail>();


            var mock = new Mock<IRepository<OrderDetail>>();

            mock.Setup(x => x.Get(It.IsAny<Expression<Func<OrderDetail, bool>>>()))
                .Returns(mockOrderDetail.Object);

            var repository = mock.Object;
            var service = new OrderDetailServiceMock(repository);
            var result = service.getOrderDetail();

            return result;
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
