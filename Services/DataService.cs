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
        private ObservableCollection<Order> _orders;
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

            _orders = new ObservableCollection<Order>();
        }
        public ObservableCollection<Product> GetProducts() => _products;
        public ObservableCollection<Order> GetOrders() => _orders;
        public void AddOrder(Order order) => _orders.Insert(0, order);

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Order> GetPendingOrders()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Order> GetDeliveredOrders()
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<HistoryItem> GetHistory()
        {
            throw new NotImplementedException();
        }

        public void AddHistoryItem(HistoryItem item)
        {
            throw new NotImplementedException();
        }
    }
}
