using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTrainningCSharp.Model;

namespace WPFTrainningCSharp.Service
{
    class OrderDetailServiceMock : IOrderDetail
    {

        private IRepository<OrderDetail> orderDetailRepository;

        public OrderDetailServiceMock(IRepository<OrderDetail> rep)
        {
            orderDetailRepository = rep;
        }
        public ObservableCollection<OrderDetail> getOrderDetail()
        {
            var orderDetail = new ObservableCollection<OrderDetail>();
            orderDetail.Add(new OrderDetail() { Name = "Khanh", Seq = 1, ItemCode = "STK000001", Description = "APPLE IPAD CASING - WHITE", UOM = "PC", UnitPrice = 100, Quantity = 30, Disc = 0.00, Tax = 1 });
            orderDetail.Add(new OrderDetail() { Name = "Khanh", Seq = 2, ItemCode = "STK000001", Description = "APPLE IPAD CASING - WHITE", UOM = "PC", UnitPrice = 90, Quantity = 30, Disc = 0.00, Tax = 1 });
            orderDetail.Add(new OrderDetail() { Name = "Khanh", Seq = 3, ItemCode = "STK000002", Description = "APPLE IPAD CASING - WHITE", UOM = "PC", UnitPrice = 160, Quantity = 30, Disc = 0.00, Tax = 1 });
            orderDetail.Add(new OrderDetail() { Name = "Gia Khanh", Seq = 4, ItemCode = "STK000003", Description = "APPLE IPAD CASING - WHITE", UOM = "PC", UnitPrice = 10, Quantity = 30, Disc = 0.00, Tax = 1 });
            orderDetail.Add(new OrderDetail() { Name = "Khanh Nguyen", Seq = 5, ItemCode = "STK000004", Description = "APPLE IPAD CASING - WHITE", UOM = "PC", UnitPrice = 260, Quantity = 30, Disc = 0.00, Tax = 1 });
            orderDetail.Add(new OrderDetail() { Name = "Khanh Nguyen", Seq = 6, ItemCode = "STK000005", Description = "APPLE IPAD CASING - WHITE", UOM = "PC", UnitPrice = 30, Quantity = 30, Disc = 0.00, Tax = 1 });
            orderDetail.Add(new OrderDetail() { Name = "Khanh Nguyen", Seq = 7, ItemCode = "STK000006", Description = "APPLE IPAD CASING - WHITE", UOM = "PC", UnitPrice = 5, Quantity = 30, Disc = 0.00, Tax = 1 });
            return orderDetail;
        }


    }
}
