using CommunityToolkit.Mvvm.Input;
using System.Windows.Media;

namespace ERP.Desktop.ViewModels;

public class DashboardViewModel : ShellPageViewModelBase
{
    public IRelayCommand RefreshCommand { get; }

    public DashboardViewModel()
    {
        SetHeaderTitle("Dashboard");
        SetStatus("Tổng quan hệ thống", Brushes.SeaGreen);

        RefreshCommand = new RelayCommand(() => SetStatus("Dashboard đã làm mới", Brushes.SeaGreen));
        AddHeaderAction("⟳", "Làm mới dashboard", RefreshCommand);
    }
}
