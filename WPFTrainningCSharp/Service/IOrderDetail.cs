using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTrainningCSharp.Model;

namespace WPFTrainningCSharp.Service
{
    public class IOrderDetail
    {
        public interface IOrderDetailService
        {
            ObservableCollection<OrderDetail> getOrderDetail();
        }
    }
}
