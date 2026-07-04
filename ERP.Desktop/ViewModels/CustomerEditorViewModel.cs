using CommunityToolkit.Mvvm.Input;
using ERP.Domain.Customers;

namespace ERP.Desktop.ViewModels;

public class CustomerEditorViewModel : ViewModelBase
{
    private string _code = string.Empty;
    private string _name = string.Empty;
    private string _phone = string.Empty;
    private string _email = string.Empty;
    private string _address = string.Empty;
    private bool _isActive = true;
    private Customer? _result;

    public CustomerEditorViewModel(Customer? customer = null)
    {
        if (customer is not null)
        {
            CustomerId = customer.Id;
            _code = customer.Code;
            _name = customer.Name;
            _phone = customer.Phone;
            _email = customer.Email;
            _address = customer.Address;
            _isActive = customer.IsActive;
            DialogTitle = "Sửa khách hàng";
        }
        else
        {
            DialogTitle = "Tạo mới khách hàng";
        }

        SaveCommand = new RelayCommand(() => Result = BuildCustomer());
        CancelCommand = new RelayCommand(() => Result = null);
    }

    public int CustomerId { get; }

    public string Code
    {
        get => _code;
        set
        {
            _code = value;
            OnPropertyChanged();
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    public string Phone
    {
        get => _phone;
        set
        {
            _phone = value;
            OnPropertyChanged();
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    public string Address
    {
        get => _address;
        set
        {
            _address = value;
            OnPropertyChanged();
        }
    }

    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            OnPropertyChanged();
        }
    }

    public string DialogTitle { get; }

    public Customer? Result
    {
        get => _result;
        private set
        {
            _result = value;
            OnPropertyChanged();
        }
    }

    public IRelayCommand SaveCommand { get; }

    public IRelayCommand CancelCommand { get; }

    private Customer BuildCustomer()
    {
        return new Customer
        {
            Id = CustomerId,
            Code = Code.Trim(),
            Name = Name.Trim(),
            Phone = Phone.Trim(),
            Email = Email.Trim(),
            Address = Address.Trim(),
            IsActive = IsActive
        };
    }
}