using BanHangVip.Models;
using BanHangVip.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanHangVip.ViewModels
{
    public partial class HistoryViewModel : BaseViewModel
    {
        private readonly IDataService _dataService;

        [ObservableProperty]
        private ObservableCollection<HistoryItem> historyItems;

        public HistoryViewModel(IDataService dataService)
        {
            Title = "Lịch sử";
            _dataService = dataService;
            LoadData();
        }

        private void LoadData()
        {
            HistoryItems = _dataService.GetHistory();
        }

        public void Refresh()
        {
            LoadData();
        }
    }
}
