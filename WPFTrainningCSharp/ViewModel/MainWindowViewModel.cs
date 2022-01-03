using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFTrainningCSharp.Command;
using WPFTrainningCSharp.Model;
using WPFTrainningCSharp.Validators;

namespace WPFTrainningCSharp.ViewModel
{
    public class MainWindowViewModel : ViewModel
    {
        private Paymentterm paymentterm;
        private PriceType priceType;
        private bool isEnable;
        private decimal sumDisc;
        private decimal addDisc;
        private string code;
        private decimal ship;
        private string sumTax;
        private decimal subVAT;
        private decimal total;
        private string noteData;
        private decimal sumAmount;
        private string totalString;
        private ObservableCollection<OrderDetail> orderDetails;
        private ObservableCollection<Person> customer;
        private ObservableCollection<Person> salepeople;
        private ObservableCollection<Person> shipto;
        private ObservableCollection<Person> billto;
        private ICommand saveCommand;
        private bool saveEnable;
        private string orderNo;
        private string currentCode;
        private DateTime dueDate;
        private DateTime shippingDate;

        public MainWindowViewModel()
        {
            Code = "NNNNNN01";
            NoteData = "Thanks for your business";
            TotalString = "US Dollar 20 thousand";
            orderDetails = OrderDetailViewModel.getOrderDetail();
        }
        public bool IsEnable
        {
            get
            {
                if (Price == PriceType.Internal_Price)
                {
                    return isEnable = false;
                }
                else
                {
                    return isEnable = true;
                }
            }
            set { isEnable = value; OnPropertyChanged(nameof(IsEnable)); }
        }
        public ObservableCollection<Person> ShipTo
        {
            get
            {
                var people = new ObservableCollection<Person>();
                shipto = new ObservableCollection<Person>();
                people = PersonViewModel.getPerson();
                foreach (var per in people)
                {
                    if (per.Roll == "ShipTo")
                    {
                        shipto.Add(per);
                    }
                }
                return shipto;
            }
            set
            {
                shipto = value;
                OnPropertyChanged("ShipTo");
            }
        }
        public ObservableCollection<Person> BillTo
        {
            get
            {
                var people = new ObservableCollection<Person>();
                billto = new ObservableCollection<Person>();
                people = PersonViewModel.getPerson();
                foreach (var per in people)
                {
                    if (per.Roll == "BillTo")
                    {
                        billto.Add(per);
                    }
                }
                return billto;
            }
            set
            {
                billto = value;
                OnPropertyChanged("BillTo");
            }
        }
        public ObservableCollection<Person> Customer
        {
            get
            {
                var people = new ObservableCollection<Person>();
                customer = new ObservableCollection<Person>();
                people = PersonViewModel.getPerson();
                foreach (var per in people)
                {
                    if (per.Roll == "Customer")
                    {
                        customer.Add(per);
                    }
                }
                return customer;
            }
            set
            {
                customer = value;
                OnPropertyChanged("Customer");
            }
        }

        public ObservableCollection<Person> SalePeople
        {
            get
            {
                var people = new ObservableCollection<Person>();
                salepeople = new ObservableCollection<Person>();
                people = PersonViewModel.getPerson();
                foreach (var per in people)
                {
                    if (per.Roll == "Sale")
                    {
                        salepeople.Add(per);
                    }
                }
                return salepeople;
            }
            set
            {
                salepeople = value;
                OnPropertyChanged("SalePeople");
            }
        }



        [Required(ErrorMessage ="Order No. can not be empty")]
        [StringLength(10, ErrorMessage = "Order No. is required and Length <= 10 chars")]
        public string OrderNo
        {
            get { return orderNo; }
            set
            {
                orderNo = value;
                ValidateProperty(value, "OrderNo");
                OnPropertyChanged("OrderNo");
                OnPropertyChanged(nameof(SaveEnable));
            }
        }
        private void ValidateProperty<T>(T value, string name)
        {
            Validator.ValidateProperty(value, new ValidationContext(this, null, null)
            {
                MemberName = name
            });
        }


        [Required(ErrorMessage = "CurrentCode can not be empty")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency is required and Length = 3 chars")]
        public string CurrentCode
        {
            get { return currentCode; }
            set
            {
                currentCode = value;
                ValidateProperty(value, "CurrentCode");
                OnPropertyChanged("CurrentCode");
                OnPropertyChanged(nameof(SaveEnable));
            }
        }
        public bool SaveEnable
        {
            get 
            {
                if (HasErrors)
                {
                    saveEnable = false;
                }
                else
                {
                    saveEnable = true;
                }
                return saveEnable;
            }
            set
            {
                saveEnable = value;
                OnPropertyChanged(nameof(SaveEnable));
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new ExcuteCommand(ShowResultSave, CanSaveCommand);
                }
                return saveCommand;
            }
        }

        public bool CanSaveCommand(object parameter)
        {
            if (!saveEnable)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ShowResultSave(object parameter)
        {
            var orderDetail = new ObservableCollection<OrderDetail>();
            orderDetail = OrderDetailViewModel.getOrderDetail();
            string value = DateBegin.ToString() + "\n" + DueDate.ToString() + "\n" + ShippingDate.ToString();
            MessageBox.Show(value, "ResultSave");
        }

        private DateTime dateBegin;
        public DateTime DateBegin
        {
            get { return dateBegin; }
            set
            {
                dateBegin = value;
                ClearErrorsOfProperty(nameof(DueDate));
                if (DueDate < DateBegin)
                {
                    AddErrorOfPropertyInErrorsList(nameof(DueDate), "Due By must be greater than Date");
                }
                ClearErrorsOfProperty(nameof(ShippingDate));
                if (ShippingDate < DateBegin)
                {
                    AddErrorOfPropertyInErrorsList(nameof(ShippingDate), "Shipping Date must be greater than Date");
                }
                OnPropertyChanged(nameof(DateBegin));
                OnPropertyChanged(nameof(SaveEnable));
            }
        }


        public DateTime DueDate
        {
            get { return dueDate; }
            set
            {
                dueDate = value;
                ClearErrorsOfProperty(nameof(DueDate));
                if (DueDate < DateBegin)
                {
                    AddErrorOfPropertyInErrorsList(nameof(DueDate), "Due By must be greater than Date");
                }
                OnPropertyChanged(nameof(DueDate));
                OnPropertyChanged(nameof(SaveEnable));
            }
        }


        public DateTime ShippingDate
        {
            get { return shippingDate; }
            set
            {
                shippingDate = value;
                ClearErrorsOfProperty(nameof(ShippingDate));
                if (shippingDate < DateBegin)
                {
                    AddErrorOfPropertyInErrorsList(nameof(ShippingDate), "Shipping Date must be greater than Date");
                }
                OnPropertyChanged("ShippingDate");
                OnPropertyChanged(nameof(SaveEnable));
            }
        }

        public decimal SumDisc
        {
            get
            {
                sumDisc = 0;
                foreach (OrderDetail item in orderDetails)
                {
                    sumDisc += item.DiscAmt;
                }
                return sumDisc;
            }
            set
            {
                sumDisc = value;
                OnPropertyChanged("SumDisc");
            }
        }



        public decimal AddDisc
        {
            get { return addDisc; }
            set
            {
                addDisc = value;
                OnPropertyChanged("AddDisc");
            }
        }

        public string Code
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged("Code");
            }
        }



        public decimal Ship
        {
            get { return ship; }
            set
            {
                ship = value;
                OnPropertyChanged("Ship");
            }
        }



        public string SumTax
        {
            get { return sumTax; }
            set
            {
                sumTax = value;
                OnPropertyChanged("SumTax");
            }
        }




        public decimal SubVAT
        {
            get
            {
                subVAT = 0;
                foreach (OrderDetail item in orderDetails)
                {
                    subVAT += item.TaxAmount;
                }
                return subVAT;
            }
            set
            {
                subVAT = value;
                OnPropertyChanged("SubVAT");
            }
        }
        public decimal Total
        {
            get { return total = (SubVAT + SumAmount - SumDisc); }
            set
            {
                total = value;
                OnPropertyChanged("Total");
            }
        }

        public decimal SumAmount
        {
            get
            {
                sumAmount = 0;
                foreach (OrderDetail item in orderDetails)
                {
                    sumAmount += item.Amount;
                }
                return sumAmount;
            }
            set
            {
                sumAmount = value;
                OnPropertyChanged("SumAmount");
            }
        }


        public string NoteData
        {
            get { return noteData; }
            set
            {
                noteData = value;
                OnPropertyChanged("NoteData");
            }
        }


        public string TotalString
        {
            get { return totalString; }
            set
            {
                totalString = value;
                OnPropertyChanged("TotalString");
            }
        }

        public PriceType Price
        {
            get => priceType;
            set
            {
                priceType = value;
                OnPropertyChanged("Price");
                OnPropertyChanged(nameof(IsEnable));
            }
        }
        public Paymentterm PaymentTerms
        {
            get { return paymentterm; }
            set
            {
                paymentterm = value;
                OnPropertyChanged("PaymentTerms");
            }
        }

    }
}
