using CommunityToolkit.Maui.Views;
using BanHangVip.Models;
using System.Collections.ObjectModel;

namespace BanHangVip.Views.Popups;

public partial class CustomerSelectionPopup : Popup
{
    public CustomerSelectionPopup(ObservableCollection<Customer> customers)
    {
        InitializeComponent();
        // Gán dữ liệu vào CollectionView
        CustomerCollection.ItemsSource = customers;
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as Customer;
        if (selected != null)
        {
            // Trả về khách hàng được chọn và đóng Popup
            Close(selected);
        }
    }
}