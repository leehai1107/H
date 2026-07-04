using CommunityToolkit.Mvvm.Input;
using System.Windows.Media;

namespace ERP.Desktop.ViewModels;

public class SettingsViewModel : ShellPageViewModelBase
{
    public IRelayCommand SaveCommand { get; }
    public IRelayCommand RefreshCommand { get; }

    public SettingsViewModel()
    {
        SetHeaderTitle("Settings");
        SetStatus("Thiết lập hệ thống", Brushes.SeaGreen);

        SaveCommand = new RelayCommand(() => SetStatus("Đã lưu cấu hình", Brushes.ForestGreen));
        RefreshCommand = new RelayCommand(() => SetStatus("Đã làm mới cài đặt", Brushes.SlateGray));

        AddHeaderAction("💾", "Lưu", SaveCommand);
        AddHeaderAction("⟳", "Làm mới", RefreshCommand);
    }
}