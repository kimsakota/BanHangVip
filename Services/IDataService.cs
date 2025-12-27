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

        ObservableCollection<Customer> GetCustomers();
        void AddCustomer(Customer customer);

        // Đơn hàng
        ObservableCollection<Order> GetOrders();
        ObservableCollection<Order> GetPendingOrders();
        ObservableCollection<Order> GetDeliveredOrders();
        void AddOrder(Order order);
        void UpdateOrder(Order order);

        // Lịch sử
        ObservableCollection<HistoryItem> GetHistory();
        void AddHistoryItem(HistoryItem item);

        void DeliverOrder(Order order);

        ObservableCollection<Order> GetUnpaidOrders();
        void ProcessPayment(string customerName);
    }
}
