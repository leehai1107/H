using CommunityToolkit.Mvvm.Input;
using System.Windows.Media;

namespace ERP.Desktop.ViewModels;

public class ProductionViewModel : ShellPageViewModelBase
{
    public IRelayCommand StartCommand { get; }
    public IRelayCommand CompleteCommand { get; }
    public IRelayCommand RefreshCommand { get; }

    public ProductionViewModel()
    {
        SetHeaderTitle("Production");
        SetStatus("Tiến độ sản xuất", Brushes.SeaGreen);

        StartCommand = new RelayCommand(() => SetStatus("Bắt đầu sản xuất", Brushes.RoyalBlue));
        CompleteCommand = new RelayCommand(() => SetStatus("Hoàn tất sản xuất", Brushes.ForestGreen));
        RefreshCommand = new RelayCommand(() => SetStatus("Đã làm mới sản xuất", Brushes.SlateGray));

        AddHeaderAction("▶", "Bắt đầu", StartCommand);
        AddHeaderAction("✓", "Hoàn tất", CompleteCommand);
        AddHeaderAction("⟳", "Làm mới", RefreshCommand);
    }
}