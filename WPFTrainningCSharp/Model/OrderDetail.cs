using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTrainningCSharp.Model
{
    public class OrderDetail : OrderInfo, INotifyPropertyChanged
    {
        private string name;
        private int seq;
        private string itemCode;
        private string description;
        private string uOM;
        private decimal unitPrice;
        private int quantity;
        private decimal amount;
        private double disc;
        private decimal discAmt;
        private decimal finalAmt;
        private int tax;
        private decimal taxAmount;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }
        public int Seq
        {
            get { return seq; }
            set
            {
                seq = value;
            }
        }
        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
            }
        }
        public string UOM { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged("Quantity");
                OnPropertyChanged(nameof(Amount));
                OnPropertyChanged(nameof(FinalAmt));
                OnPropertyChanged(nameof(TaxAmount));
            }
        }
        public decimal Amount
        {
            get { return amount = Quantity * UnitPrice; }
            set
            {
                amount = Quantity * UnitPrice;
                OnPropertyChanged("Amount");
            }
        }
        public double Disc
        {
            get { return disc; }
            set
            {
                disc = value;
                OnPropertyChanged("Disc");
                OnPropertyChanged(nameof(FinalAmt));
            }
        }
        public decimal DiscAmt
        {
            get { return discAmt = (decimal)Disc * Amount; }
            set
            {
                discAmt = (decimal)Disc * Amount;
                OnPropertyChanged("DiscAmt");
            }
        }
        public decimal FinalAmt
        {
            get { return finalAmt = Amount - DiscAmt; }
            set
            {
                finalAmt = Amount - DiscAmt;
                OnPropertyChanged("FinalAmt");
            }
        }
        public int Tax { get; set; }
        public decimal TaxAmount
        {
            get
            {
                return taxAmount = FinalAmt * Tax * 5 / 100;
            }
            set
            {
                taxAmount = FinalAmt * Tax * 5 / 100;
                OnPropertyChanged("TaxAmount");
            }
        }
        //Contractor không có tham số
        public OrderDetail(){}


        //Contractor có tham số
        public OrderDetail(string Name, int Seq, string ItemCode, string Description, string UOM, decimal UnitPrice, int Quantity, double Disc, int Tax)
        {
            name = Name;
            seq = Seq;
            itemCode = ItemCode;
            description = Description;
            uOM = UOM;
            unitPrice = UnitPrice;
            quantity = Quantity;
            disc = Disc;
            tax = Tax;
        }
        public override string ToString()
        {
            string output = $"Order Detail: \n" +
                                $"      Item Code: {ItemCode} \n" +
                                $"      Description: {Description} \n" +
                                $"      U.O.M: {UOM} \n" +
                                $"      Unit Price: {UnitPrice} \n" +
                                $"      Quantity: {Quantity} \n" +
                                $"      Amount: {Amount} \n" +
                                $"      Disc%: {DiscAmt}% \n" +
                                $"      DiscAmount: {DiscAmt} \n" +
                                $"      Final Amt: {FinalAmt} \n" +
                                $"      Tax: {Tax} \n" +
                                $"      Tax Amount: {TaxAmount} \n";
            return output;
        }
    }
}
