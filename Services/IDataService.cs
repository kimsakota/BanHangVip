using BanHangVip.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanHangVip.Services
{
    public interface IDataService
    {
        // Sản phẩm
        ObservableCollection<Product> GetProducts();
        void UpdateProduct(Product product);

        // Đơn hàng
        ObservableCollection<Order> GetOrders();
        ObservableCollection<Order> GetPendingOrders();
        ObservableCollection<Order> GetDeliveredOrders();
        void AddOrder(Order order);
        void UpdateOrder(Order order);

        // Lịch sử
        ObservableCollection<HistoryItem> GetHistory();
        void AddHistoryItem(HistoryItem item);
    }
}
