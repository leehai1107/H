using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;
using ERP.Application.Purchases;
using ERP.Desktop.Services;
using ERP.Domain.Purchases;
using ERP.Infrastructure.Purchases;

namespace ERP.Desktop.ViewModels;

public class PurchaseViewModel : ShellPageViewModelBase
{
    private readonly IPurchaseService _purchaseService;
    private readonly IDialogService _dialogService;
    private readonly ICollectionView _purchaseView;
    private string _searchText = string.Empty;
    private PurchaseOrder? _selectedOrder;

    public ObservableCollection<PurchaseOrder> PurchaseOrders { get; } = new();

    public ICollectionView PurchaseView => _purchaseView;

    public IRelayCommand NewCommand { get; }
    public IRelayCommand AddCommand { get; }
    public IRelayCommand DeleteCommand { get; }
    public IRelayCommand RefreshCommand { get; }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText == value)
            {
                return;
            }

            _searchText = value;
            OnPropertyChanged();
            _purchaseView.Refresh();
            OnPropertyChanged(nameof(TotalOrders));
            OnPropertyChanged(nameof(ActiveOrders));
        }
    }

    public PurchaseOrder? SelectedOrder
    {
        get => _selectedOrder;
        set
        {
            _selectedOrder = value;
            OnPropertyChanged();
        }
    }

    public int TotalOrders => PurchaseOrders.Count;

    public int ActiveOrders => PurchaseOrders.Count(order => order.IsActive);

    public PurchaseViewModel()
        : this(new PurchaseService(new InMemoryPurchaseRepository()), new DialogService())
    {
    }

    public PurchaseViewModel(IPurchaseService purchaseService, IDialogService dialogService)
    {
        _purchaseService = purchaseService;
        _dialogService = dialogService;

        SetHeaderTitle("Purchase");
        SetStatus("Nghiệp vụ mua hàng", Brushes.SeaGreen);

        _purchaseView = CollectionViewSource.GetDefaultView(PurchaseOrders);
        _purchaseView.Filter = FilterPurchaseOrder;

        NewCommand = new RelayCommand(CreatePurchaseOrder);
        AddCommand = new RelayCommand(CreatePurchaseOrder);
        DeleteCommand = new RelayCommand(() =>
        {
            if (SelectedOrder is null)
            {
                SetStatus("Chọn phiếu mua để xóa", Brushes.IndianRed);
                return;
            }

            DeletePurchaseOrder(SelectedOrder);
        });
        RefreshCommand = new RelayCommand(() =>
        {
            LoadPurchaseOrders();
            SetStatus("Đã làm mới mua hàng", Brushes.SlateGray);
        });

        AddHeaderAction("＋", "Tạo mới", NewCommand);
        AddHeaderAction("➕", "Thêm", AddCommand);
        AddHeaderAction("🗑", "Xóa", DeleteCommand);
        AddHeaderAction("⟳", "Làm mới", RefreshCommand);

        LoadPurchaseOrders();
    }

    private void LoadPurchaseOrders()
    {
        PurchaseOrders.Clear();

        foreach (var order in _purchaseService.GetPurchaseOrders())
        {
            PurchaseOrders.Add(order);
        }

        _purchaseView.Refresh();
        OnPropertyChanged(nameof(TotalOrders));
        OnPropertyChanged(nameof(ActiveOrders));
    }

    private void CreatePurchaseOrder()
    {
        var createdOrder = _dialogService.ShowPurchaseEditor();
        if (createdOrder is null)
        {
            SetStatus("Đã hủy tạo phiếu mua", Brushes.SlateGray);
            return;
        }

        _purchaseService.AddPurchaseOrder(createdOrder);
        LoadPurchaseOrders();
        SetStatus($"Đã tạo phiếu mua: {createdOrder.Code}", Brushes.ForestGreen);
    }

    private void DeletePurchaseOrder(PurchaseOrder selectedOrder)
    {
        var confirm = MessageBox.Show(
            $"Bạn có chắc muốn xóa phiếu mua {selectedOrder.Code} - {selectedOrder.SupplierName}?",
            "Xác nhận xóa",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (confirm != MessageBoxResult.Yes)
        {
            SetStatus("Đã hủy xóa phiếu mua", Brushes.SlateGray);
            return;
        }

        _purchaseService.DeletePurchaseOrder(selectedOrder.Id);
        LoadPurchaseOrders();
        SelectedOrder = null;
        SetStatus($"Đã xóa phiếu mua: {selectedOrder.Code}", Brushes.IndianRed);
    }

    private bool FilterPurchaseOrder(object item)
    {
        if (item is not PurchaseOrder order)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(SearchText))
        {
            return true;
        }

        var search = SearchText.Trim();

        return order.Code.Contains(search, StringComparison.OrdinalIgnoreCase)
            || order.SupplierName.Contains(search, StringComparison.OrdinalIgnoreCase)
            || order.Status.Contains(search, StringComparison.OrdinalIgnoreCase);
    }
}