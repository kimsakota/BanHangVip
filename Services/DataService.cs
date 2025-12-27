using BanHangVip.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanHangVip.Services
{
    public class DataService : IDataService
    {
        private ObservableCollection<Product> _products;
        private ObservableCollection<Customer> _customers;
        private ObservableCollection<Order> _orders;
        private ObservableCollection<HistoryItem> _history;

        public DataService()
        {
            _products = new ObservableCollection<Product>
            {
                new Product { Id = "1", Name = "Hàu", Icon = "🦪", DefaultPrice = 150000 },
                new Product { Id = "2", Name = "Tôm", Icon = "🦐", DefaultPrice = 350000 },
                new Product { Id = "3", Name = "Cá Mú", Icon = "🐟", DefaultPrice = 420000 },
                new Product { Id = "4", Name = "Cá Vượt", Icon = "🐟", DefaultPrice = 180000 },
                new Product { Id = "5", Name = "Ốc Hương", Icon = "🐚", DefaultPrice = 320000 },
                new Product { Id = "6", Name = "Cua Thịt", Icon = "🦀", DefaultPrice = 480000 },
            };

            _customers = new ObservableCollection<Customer>
            {
                new Customer { Id = "1", Name = "Nhà Mai", Phone = "098xxx", Avatar = "M" },
                new Customer { Id = "2", Name = "Nhà Nguyệt", Phone = "091xxx", Avatar = "N" },
                new Customer { Id = "3", Name = "Nhà Thọ", Phone = "091xxx", Avatar = "T" },
                new Customer { Id = "4", Name = "Khách Lẻ", Phone = "", Avatar = "K" }
            };

            _orders = new ObservableCollection<Order>();
            _history = new ObservableCollection<HistoryItem>();
        }
        public ObservableCollection<Product> GetProducts() => _products;
        public ObservableCollection<Order> GetOrders() => _orders;
        public void AddOrder(Order order) => _orders.Insert(0, order);

        public ObservableCollection<Customer> GetCustomers() => _customers;
        public void AddCustomer(Customer customer) => _customers.Add(customer);

        public void DeleteCustomer(Customer customer)
        {
            if (_customers.Contains(customer))
            {
                _customers.Remove(customer);
            }
        }

        public void UpdateProduct(Product product)
        {
            var existing = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                var index = _products.IndexOf(existing);
                _products[index] = product;
            }
        }

        public ObservableCollection<Order> GetPendingOrders()
        {
            return new ObservableCollection<Order>(_orders.Where(o => o.Status == OrderStatus.Pending));
        }

        public ObservableCollection<Order> GetDeliveredOrders()
        {
            return new ObservableCollection<Order>(_orders.Where(o => o.Status == OrderStatus.Delivered));
        }

        public void UpdateOrder(Order order)
        {
            var existing = _orders.FirstOrDefault(o => o.Id == order.Id);
            if (existing != null)
            {
                var index = _orders.IndexOf(existing);
                _orders[index] = order;
            }
        }

        public ObservableCollection<HistoryItem> GetHistory() => _history;

        public void AddHistoryItem(HistoryItem item)
        {
            _history.Insert(0, item);
        }

        public void DeliverOrder(Order order)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.Id == order.Id);
            if (existingOrder != null)
            {
                existingOrder.Status = OrderStatus.Delivered;
                // Cập nhật thời gian giao thực tế nếu cần
                // existingOrder.DeliveredAt = DateTime.Now; 

                // Tạo lịch sử xuất kho cho từng món trong đơn
                foreach (var item in existingOrder.Items)
                {
                    var historyItem = new HistoryItem
                    {
                        Id = $"DEL-{DateTime.Now.Ticks}-{item.ProductId}",
                        Type = "DELIVERY",
                        ProductName = item.ProductName,
                        Weight = item.Weight,
                        Price = item.Price,
                        Timestamp = DateTime.Now
                    };
                    _history.Insert(0, historyItem);
                }
            }
        }

        public ObservableCollection<Order> GetUnpaidOrders()
        {
            return new ObservableCollection<Order>(
                _orders.Where(o => o.Status == OrderStatus.Delivered && !o.IsPaid)
            );
        }

        public void ProcessPayment(string customerName)
        {
            var unpaidOrders = _orders
                .Where(o => o.Status == OrderStatus.Delivered && !o.IsPaid && o.CustomerName == customerName)
                .ToList();

            foreach (var order in unpaidOrders)
            {
                order.IsPaid = true; // Đánh dấu đã trả

                // Ghi lịch sử thu tiền
                foreach (var item in order.Items)
                {
                    _history.Insert(0, new HistoryItem
                    {
                        Id = $"PAY-{DateTime.Now.Ticks}-{item.ProductId}",
                        Type = "PAYMENT",
                        ProductName = item.ProductName,
                        Weight = item.Weight,
                        Price = item.Price,
                        Timestamp = DateTime.Now
                    });
                }
            }
        }
    }
}
