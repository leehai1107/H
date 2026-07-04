using CommunityToolkit.Mvvm.Input;
using ERP.Domain.Purchases;

namespace ERP.Desktop.ViewModels;

public class PurchaseEditorViewModel : ViewModelBase
{
    private string _code = string.Empty;
    private string _supplierName = string.Empty;
    private DateTime _orderDate = DateTime.Today;
    private decimal _amount;
    private string _status = string.Empty;
    private bool _isActive = true;
    private PurchaseOrder? _result;

    public PurchaseEditorViewModel(PurchaseOrder? order = null)
    {
        if (order is not null)
        {
            OrderId = order.Id;
            _code = order.Code;
            _supplierName = order.SupplierName;
            _orderDate = order.OrderDate;
            _amount = order.Amount;
            _status = order.Status;
            _isActive = order.IsActive;
            DialogTitle = "Sửa phiếu mua";
        }
        else
        {
            DialogTitle = "Tạo mới phiếu mua";
        }

        SaveCommand = new RelayCommand(() => Result = BuildOrder());
        CancelCommand = new RelayCommand(() => Result = null);
    }

    public int OrderId { get; }

    public string Code
    {
        get => _code;
        set { _code = value; OnPropertyChanged(); }
    }

    public string SupplierName
    {
        get => _supplierName;
        set { _supplierName = value; OnPropertyChanged(); }
    }

    public DateTime OrderDate
    {
        get => _orderDate;
        set { _orderDate = value; OnPropertyChanged(); }
    }

    public decimal Amount
    {
        get => _amount;
        set { _amount = value; OnPropertyChanged(); }
    }

    public string Status
    {
        get => _status;
        set { _status = value; OnPropertyChanged(); }
    }

    public bool IsActive
    {
        get => _isActive;
        set { _isActive = value; OnPropertyChanged(); }
    }

    public string DialogTitle { get; }

    public PurchaseOrder? Result
    {
        get => _result;
        private set { _result = value; OnPropertyChanged(); }
    }

    public IRelayCommand SaveCommand { get; }

    public IRelayCommand CancelCommand { get; }

    private PurchaseOrder BuildOrder()
    {
        return new PurchaseOrder
        {
            Id = OrderId,
            Code = Code.Trim(),
            SupplierName = SupplierName.Trim(),
            OrderDate = OrderDate,
            Amount = Amount,
            Status = Status.Trim(),
            IsActive = IsActive
        };
    }
}
