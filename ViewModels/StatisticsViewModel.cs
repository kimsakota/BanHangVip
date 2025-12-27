using CommunityToolkit.Mvvm.ComponentModel;
using BanHangVip.Services;
using System.Linq;

namespace BanHangVip.ViewModels;

public partial class StatisticsViewModel : BaseViewModel
{
    private readonly IDataService _dataService;

    [ObservableProperty]
    private decimal totalRevenue; // Doanh thu thực tế (đã thu tiền)

    [ObservableProperty]
    private decimal totalImportCost; // Vốn nhập hàng

    [ObservableProperty]
    private decimal estimatedProfit; // Lợi nhuận

    [ObservableProperty]
    private double totalWeightSold;

    [ObservableProperty]
    private double totalWeightImport;

    public StatisticsViewModel(IDataService dataService)
    {
        Title = "Thống kê";
        _dataService = dataService;
        LoadStats();
    }

    public void Refresh()
    {
        LoadStats();
    }

    private void LoadStats()
    {
        var history = _dataService.GetHistory();

        // Tính toán từ lịch sử
        // INTAKE: Nhập hàng (Chi phí)
        var intakes = history.Where(h => h.Type == "INTAKE").ToList();
        TotalWeightImport = intakes.Sum(h => h.Weight);
        TotalImportCost = intakes.Sum(h => (decimal)h.Weight * h.Price);

        // PAYMENT: Thu tiền (Doanh thu)
        var payments = history.Where(h => h.Type == "PAYMENT").ToList();
        TotalRevenue = payments.Sum(h => (decimal)h.Weight * h.Price);

        // DELIVERY: Xuất hàng (Khối lượng bán)
        var deliveries = history.Where(h => h.Type == "DELIVERY").ToList();
        TotalWeightSold = deliveries.Sum(h => h.Weight);

        EstimatedProfit = TotalRevenue - TotalImportCost;
    }
}