using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFTraining.Model;
using WPFTraining.ViewModel;

namespace WPFTraining
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //List<string> PriceLevel;


        List<string> PaymentTerms;

        private string sumDisc;

        public string SumDisc
        {
            get { return sumDisc; }
            set
            {
                sumDisc = value;
                OnPropertyChanged("SumDisc");
            }
        }

        private decimal addDisc;

        public decimal AddDisc
        {
            get { return addDisc; }
            set
            {
                addDisc = value;
                OnPropertyChanged("AddDisc");
            }
        }

        private string code;

        public string Code
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged("Code");
            }
        }

        private decimal ship;

        public decimal Ship
        {
            get { return ship; }
            set
            {
                ship = value;
                OnPropertyChanged("Ship");
            }
        }

        private string sumTax;

        public string SumTax
        {
            get { return sumTax; }
            set
            {
                sumTax = value;
                OnPropertyChanged("SumTax");
            }
        }

        private PriceType _priceType;
        public PriceType Price
        {
            get => _priceType;
            set
            {
                if (_priceType != value)
                {
                    _priceType = value;
                    OnPropertyChanged("Price");
                }
            }
        }

        private string subVAT;
        public string SubVAT
        {
            get { return subVAT; }
            set
            {
                subVAT = value;
                OnPropertyChanged("SubVAT");
            }
        }

        private string total;
        public string Total
        {
            get { return total; }
            set
            {
                total = value;
                OnPropertyChanged("Total");
            }
        }

        private string sumAmount;
        public string SumAmount
        {
            get { return sumAmount; }
            set
            {
                sumAmount = value;
                OnPropertyChanged("SumAmount");
            }
        }

        private string noteData;
        public string NoteData
        {
            get { return noteData; }
            set
            {
                noteData = value;
                OnPropertyChanged("NoteData");
            }
        }

        private string totalString;
        public string TotalString
        {
            get { return totalString; }
            set
            {
                totalString = value;
                OnPropertyChanged("TotalString");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newParameter)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newParameter));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            //PriceLevel = new List<string>() { "1 – Regular Price", "2 – Wholesale Price", "3 – Internal Price" };
            //cbPriceLevel.ItemsSource = PriceLevel;

            PaymentTerms = new List<string>() { "0 Days", "7 Days", "30 Days" };
            cbPaymentterms.ItemsSource = PaymentTerms;


            txtCode.DataContext = Code;
            Code = "NNNNNN01";

            txtnote.DataContext = NoteData;
            NoteData = "Thanks for your business";

            txttotalString.DataContext = TotalString;
            TotalString = "US Dollar 20 thousand";

            //DataContext = new OrderDetailViewModel();
            //gridOrderDetail.ItemsSource = OrderDetailViewModel.getOrderDetail();
            cbPriceLevel.SelectionChanged += cbPriceLevel_SelectionChanged;

            var people = new ObservableCollection<Person>();
            var customer = new ObservableCollection<Person>();
            var salepeople = new ObservableCollection<Person>();
            people = PersonViewModel.getPerson();
            foreach (var per in people)
            {
                if (per.Roll == "Customer")
                {
                    customer.Add(per);
                }
                else if (per.Roll == "Sale")
                {
                    salepeople.Add(per);
                }
            }
            cbCustomer.ItemsSource = customer;
            cbCustomer.DisplayMemberPath = "Name";
            cbCustomer.SelectedValuePath = "Name";
            cbCustomer.SelectionChanged += cbCustomer_SelectionChanged;

            cbSalePerson.ItemsSource = salepeople;
            cbSalePerson.DisplayMemberPath = "Name";

            txtSubtotal.IsEnabled = false;
            txtVAT.IsEnabled = false;
            txtCurrentCode.Text = "USD";
            txtOrderNo.Text = "NNNNNN001";

            SumAmount = SubTotal().ToString();
            SubVAT = SumTaxAmount().ToString();
            SumDisc = SumDiscount().ToString();
            AddDisc = 0.00m;
            Ship = 0.00m;
            SumTax = "0.00";
            Total = (SumTaxAmount() + SubTotal() - SumDiscount() - AddDisc - Ship).ToString();

            dbDate.SelectedDate = DateTime.Now;
            dbDueBy.SelectedDate = DateTime.Now;
            dbShippingDate.SelectedDate = DateTime.Now;



        }

        public decimal SubTotal()
        {
            // gridOrderDetail.ItemsSource = OrderDetailViewModel.getOrderDetail();
            decimal sum = 0;
            for (int i = 0; i < gridOrderDetail.Items.Count; ++i)
            {
                OrderDetail order = gridOrderDetail.Items[i] as OrderDetail;
                if (order != null)
                    sum += order.Amount;
            }
            return sum;
        }
        public decimal SumTaxAmount()
        {
            // gridOrderDetail.ItemsSource = OrderDetailViewModel.getOrderDetail();
            decimal sum = 0;
            for (int i = 0; i < gridOrderDetail.Items.Count; ++i)
            {
                OrderDetail order = gridOrderDetail.Items[i] as OrderDetail;
                if (order != null)
                    sum += order.TaxAmount;
            }
            return sum;
        }

        public decimal SumDiscount()
        {
            // gridOrderDetail.ItemsSource = OrderDetailViewModel.getOrderDetail();
            decimal sum = 0;
            for (int i = 0; i < gridOrderDetail.Items.Count; ++i)
            {
                OrderDetail order = gridOrderDetail.Items[i] as OrderDetail;
                if (order != null)
                    sum += order.DiscAmt;
            }
            return sum;
        }


        private void cbPriceLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PriceType price = PriceType.Internal_Price;
            if (cbPriceLevel.SelectedValue.ToString() == price.GetType().GetMember(price.ToString())[0].GetCustomAttribute<DescriptionAttribute>().Description)
            {
                cbBillto.IsEnabled = false;
                cbPaymentterms.IsEnabled = false;
                txtPaymentMethod.IsEnabled = false;
            }
            else
            {
                cbBillto.IsEnabled = true;
                cbPaymentterms.IsEnabled = true;
                txtPaymentMethod.IsEnabled = true;
            }
        }

        private void cbCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var orderDetail = new ObservableCollection<OrderDetail>();
            orderDetail = OrderDetailViewModel.getOrderDetail();
            var orderCustomer = new ObservableCollection<OrderDetail>();
            foreach (var order in orderDetail)
            {
                if (cbCustomer.SelectedValue.ToString() == order.Name)
                {
                    orderCustomer.Add(order);
                }
            }
            gridOrderDetail.ItemsSource = orderCustomer;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(new ValidationViewModel().ErrorCollection != null)
            {
                btnSave.IsEnabled = false;
            }
            else
            {
                btnSave.IsEnabled = true;
            }
            string showValue = dbDate.ToString() + "\n" + dbDueBy.ToString() + "\n" + dbShippingDate.ToString();
            MessageBox.Show(showValue, "Current Value");
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class DisplayAttribute : Attribute
    {
        public DisplayAttribute(string displayName)
        {
            Description = displayName;
        }

        public string Description { get; set; }
    }
    public enum PriceType
    {
        [Description("Regular-Price")]
        Regular_Price = 0,
        [Description("Wholesale-Price")]
        Wholesale_Price = 1,
        [Description("Internal-Price")]
        Internal_Price = 2
    }
    public class PriceConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PriceType format)
            {
                return GetString(format);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                return s;
            }
            return null;

        }
        public string[] Strings => GetStrings();

        public static string GetString(PriceType format)
        {
            return GetDescription(format);
            //return format.ToString() + ": " + GetDescription(format);
            //return format.ToString();
        }

        public static string GetDescription(PriceType format)
        {
            //return format.GetType().GetMember(format.ToString())[0].GetCustomAttribute<DescriptionAttribute>().Description;
            return format.ToString().Replace("_", "-");
        }
        public static string[] GetStrings()
        {
            List<string> list = new List<string>();
            foreach (PriceType format in Enum.GetValues(typeof(PriceType)))
            {
                list.Add(GetString(format));
            }

            return list.ToArray();
        }

    }
}
