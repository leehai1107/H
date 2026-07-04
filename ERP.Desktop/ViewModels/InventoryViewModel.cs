using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;
using ERP.Application.Inventory;
using ERP.Desktop.Services;
using ERP.Domain.Inventory;
using ERP.Infrastructure.Inventory;

namespace ERP.Desktop.ViewModels;

public class InventoryViewModel : ShellPageViewModelBase
{
    private readonly IInventoryService _inventoryService;
    private readonly IDialogService _dialogService;
    private readonly ICollectionView _inventoryView;
    private string _searchText = string.Empty;
    private InventoryItem? _selectedItem;

    public ObservableCollection<InventoryItem> InventoryItems { get; } = new();

    public ICollectionView InventoryView => _inventoryView;

    public IRelayCommand AddCommand { get; }
    public IRelayCommand EditCommand { get; }
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
            _inventoryView.Refresh();
            OnPropertyChanged(nameof(TotalItems));
            OnPropertyChanged(nameof(LowStockItems));
        }
    }

    public InventoryItem? SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            OnPropertyChanged();
        }
    }

    public int TotalItems => InventoryItems.Count;

    public int LowStockItems => InventoryItems.Count(item => item.Quantity <= item.MinQuantity);

    public InventoryViewModel()
        : this(new InventoryService(new InMemoryInventoryRepository()), new DialogService())
    {
    }

    public InventoryViewModel(IInventoryService inventoryService, IDialogService dialogService)
    {
        _inventoryService = inventoryService;
        _dialogService = dialogService;

        SetHeaderTitle("Inventory");
        SetStatus("Danh mục tồn kho", Brushes.SeaGreen);

        _inventoryView = CollectionViewSource.GetDefaultView(InventoryItems);
        _inventoryView.Filter = FilterInventoryItem;

        AddCommand = new RelayCommand(CreateInventoryItem);
        EditCommand = new RelayCommand(() =>
        {
            if (SelectedItem is null)
            {
                SetStatus("Chọn vật tư để sửa", Brushes.DarkOrange);
                return;
            }

            EditInventoryItem(SelectedItem);
        });
        DeleteCommand = new RelayCommand(() =>
        {
            if (SelectedItem is null)
            {
                SetStatus("Chọn vật tư để xóa", Brushes.IndianRed);
                return;
            }

            DeleteInventoryItem(SelectedItem);
        });
        RefreshCommand = new RelayCommand(() =>
        {
            LoadInventoryItems();
            SetStatus("Đã làm mới kho", Brushes.SlateGray);
        });

        AddHeaderAction("➕", "Thêm", AddCommand);
        AddHeaderAction("✎", "Sửa", EditCommand);
        AddHeaderAction("🗑", "Xóa", DeleteCommand);
        AddHeaderAction("⟳", "Làm mới", RefreshCommand);

        LoadInventoryItems();
    }

    private void LoadInventoryItems()
    {
        InventoryItems.Clear();

        foreach (var item in _inventoryService.GetInventoryItems())
        {
            InventoryItems.Add(item);
        }

        _inventoryView.Refresh();
        OnPropertyChanged(nameof(TotalItems));
        OnPropertyChanged(nameof(LowStockItems));
    }

    private void CreateInventoryItem()
    {
        var createdItem = _dialogService.ShowInventoryEditor();
        if (createdItem is null)
        {
            SetStatus("Đã hủy tạo vật tư", Brushes.SlateGray);
            return;
        }

        _inventoryService.AddInventoryItem(createdItem);
        LoadInventoryItems();
        SetStatus($"Đã tạo vật tư: {createdItem.Code}", Brushes.ForestGreen);
    }

    private void EditInventoryItem(InventoryItem selectedItem)
    {
        var updatedItem = _dialogService.ShowInventoryEditor(selectedItem);
        if (updatedItem is null)
        {
            SetStatus("Đã hủy sửa vật tư", Brushes.SlateGray);
            return;
        }

        _inventoryService.UpdateInventoryItem(updatedItem);
        LoadInventoryItems();
        SelectedItem = InventoryItems.FirstOrDefault(item => item.Id == updatedItem.Id);
        SetStatus($"Đã cập nhật vật tư: {updatedItem.Code}", Brushes.DarkOrange);
    }

    private void DeleteInventoryItem(InventoryItem selectedItem)
    {
        var confirm = MessageBox.Show(
            $"Bạn có chắc muốn xóa vật tư {selectedItem.Code} - {selectedItem.Name}?",
            "Xác nhận xóa",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (confirm != MessageBoxResult.Yes)
        {
            SetStatus("Đã hủy xóa vật tư", Brushes.SlateGray);
            return;
        }

        _inventoryService.DeleteInventoryItem(selectedItem.Id);
        LoadInventoryItems();
        SelectedItem = null;
        SetStatus($"Đã xóa vật tư: {selectedItem.Code}", Brushes.IndianRed);
    }

    private bool FilterInventoryItem(object item)
    {
        if (item is not InventoryItem inventoryItem)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(SearchText))
        {
            return true;
        }

        var search = SearchText.Trim();

        return inventoryItem.Code.Contains(search, StringComparison.OrdinalIgnoreCase)
            || inventoryItem.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
            || inventoryItem.Unit.Contains(search, StringComparison.OrdinalIgnoreCase);
    }
}