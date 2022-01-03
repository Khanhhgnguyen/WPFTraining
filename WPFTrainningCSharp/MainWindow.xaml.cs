using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using WPFTrainningCSharp.Model;
using WPFTrainningCSharp.ViewModel;

namespace WPFTrainningCSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //List<string> PriceLevel;


        //    List<string> PaymentTerms;

        public MainWindow()
        {
            InitializeComponent();

            cbCustomer.DisplayMemberPath = "Name";
            cbBillto.DisplayMemberPath = "Name";
            cbShippingto.DisplayMemberPath = "Name";
            cbCustomer.SelectedValuePath = "Name";
            cbCustomer.SelectionChanged += cbCustomer_SelectionChanged;

            cbSalePerson.DisplayMemberPath = "Name";


            txtCurrentCode.Text = "USD";
            txtOrderNo.Text = "NNNNNN001";



            dbDate.SelectedDate = DateTime.Now;
            dbDueBy.SelectedDate = DateTime.Now;
            dbShippingDate.SelectedDate = DateTime.Now;



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
}
