using CommunityToolkit.Mvvm.Input;
using ERP.Domain.Inventory;

namespace ERP.Desktop.ViewModels;

public class InventoryEditorViewModel : ViewModelBase
{
    private string _code = string.Empty;
    private string _name = string.Empty;
    private string _unit = string.Empty;
    private decimal _quantity;
    private decimal _minQuantity;
    private bool _isActive = true;
    private InventoryItem? _result;

    public InventoryEditorViewModel(InventoryItem? item = null)
    {
        if (item is not null)
        {
            ItemId = item.Id;
            _code = item.Code;
            _name = item.Name;
            _unit = item.Unit;
            _quantity = item.Quantity;
            _minQuantity = item.MinQuantity;
            _isActive = item.IsActive;
            DialogTitle = "Sửa vật tư";
        }
        else
        {
            DialogTitle = "Tạo mới vật tư";
        }

        SaveCommand = new RelayCommand(() => Result = BuildItem());
        CancelCommand = new RelayCommand(() => Result = null);
    }

    public int ItemId { get; }

    public string Code
    {
        get => _code;
        set { _code = value; OnPropertyChanged(); }
    }

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }

    public string Unit
    {
        get => _unit;
        set { _unit = value; OnPropertyChanged(); }
    }

    public decimal Quantity
    {
        get => _quantity;
        set { _quantity = value; OnPropertyChanged(); }
    }

    public decimal MinQuantity
    {
        get => _minQuantity;
        set { _minQuantity = value; OnPropertyChanged(); }
    }

    public bool IsActive
    {
        get => _isActive;
        set { _isActive = value; OnPropertyChanged(); }
    }

    public string DialogTitle { get; }

    public InventoryItem? Result
    {
        get => _result;
        private set { _result = value; OnPropertyChanged(); }
    }

    public IRelayCommand SaveCommand { get; }

    public IRelayCommand CancelCommand { get; }

    private InventoryItem BuildItem()
    {
        return new InventoryItem
        {
            Id = ItemId,
            Code = Code.Trim(),
            Name = Name.Trim(),
            Unit = Unit.Trim(),
            Quantity = Quantity,
            MinQuantity = MinQuantity,
            IsActive = IsActive
        };
    }
}