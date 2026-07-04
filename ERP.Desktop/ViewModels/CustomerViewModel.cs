using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;
using ERP.Application.Customers;
using ERP.Domain.Customers;
using ERP.Infrastructure.Customers;
using ERP.Desktop.Services;

namespace ERP.Desktop.ViewModels;

public class CustomerViewModel : ShellPageViewModelBase
{
    private readonly ICustomerService _customerService;
    private readonly IDialogService _dialogService;
    private readonly ICollectionView _customersView;
    private string _searchText = string.Empty;
    private Customer? _selectedCustomer;

    public ObservableCollection<Customer> Customers { get; } = new();

    public ICollectionView CustomersView => _customersView;

    public IRelayCommand ClearSearchCommand { get; }
    public IRelayCommand NewCommand { get; }
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
            _customersView.Refresh();
            OnPropertyChanged(nameof(TotalCustomers));
            OnPropertyChanged(nameof(ActiveCustomers));
        }
    }

    public Customer? SelectedCustomer
    {
        get => _selectedCustomer;
        set
        {
            _selectedCustomer = value;
            OnPropertyChanged();
        }
    }

    public int TotalCustomers => Customers.Count;

    public int ActiveCustomers => Customers.Count(customer => customer.IsActive);

    public CustomerViewModel()
        : this(new CustomerService(new InMemoryCustomerRepository()), new DialogService())
    {
    }

    public CustomerViewModel(ICustomerService customerService, IDialogService dialogService)
    {
        _customerService = customerService;
        _dialogService = dialogService;

        _customersView = CollectionViewSource.GetDefaultView(Customers);
        _customersView.Filter = FilterCustomer;

        SetHeaderTitle("Customer");
        SetStatus("Danh sách khách hàng", Brushes.SeaGreen);

        NewCommand = new RelayCommand(CreateCustomer);
        AddCommand = new RelayCommand(CreateCustomer);
        EditCommand = new RelayCommand(() =>
        {
            if (SelectedCustomer is null)
            {
                SetStatus("Chọn khách hàng để sửa", Brushes.DarkOrange);
                return;
            }

            EditCustomer(SelectedCustomer);
        });
        DeleteCommand = new RelayCommand(() =>
        {
            if (SelectedCustomer is null)
            {
                SetStatus("Chọn khách hàng để xóa", Brushes.IndianRed);
                return;
            }

            DeleteCustomer(SelectedCustomer);
        });
        RefreshCommand = new RelayCommand(() =>
        {
            LoadCustomers();
            SetStatus("Đã làm mới danh sách khách hàng", Brushes.SlateGray);
        });

        ClearSearchCommand = new RelayCommand(() => SearchText = string.Empty);

        AddHeaderAction("＋", "Tạo mới", NewCommand);
        AddHeaderAction("➕", "Thêm", AddCommand);
        AddHeaderAction("✎", "Sửa", EditCommand);
        AddHeaderAction("🗑", "Xóa", DeleteCommand);
        AddHeaderAction("⟳", "Làm mới", RefreshCommand);

        LoadCustomers();
    }

    private void LoadCustomers()
    {
        Customers.Clear();

        foreach (var customer in _customerService.GetCustomers())
        {
            Customers.Add(customer);
        }

        _customersView.Refresh();
        OnPropertyChanged(nameof(TotalCustomers));
        OnPropertyChanged(nameof(ActiveCustomers));
    }

    private void CreateCustomer()
    {
        var createdCustomer = _dialogService.ShowCustomerEditor();
        if (createdCustomer is null)
        {
            SetStatus("Đã hủy tạo khách hàng", Brushes.SlateGray);
            return;
        }

        _customerService.AddCustomer(createdCustomer);
        LoadCustomers();
        SetStatus($"Đã tạo khách hàng: {createdCustomer.Code}", Brushes.ForestGreen);
    }

    private void EditCustomer(Customer selectedCustomer)
    {
        var updatedCustomer = _dialogService.ShowCustomerEditor(selectedCustomer);
        if (updatedCustomer is null)
        {
            SetStatus("Đã hủy sửa khách hàng", Brushes.SlateGray);
            return;
        }

        _customerService.UpdateCustomer(updatedCustomer);
        LoadCustomers();
        SelectedCustomer = Customers.FirstOrDefault(customer => customer.Id == updatedCustomer.Id);
        SetStatus($"Đã cập nhật khách hàng: {updatedCustomer.Code}", Brushes.DarkOrange);
    }

    private void DeleteCustomer(Customer selectedCustomer)
    {
        var confirm = MessageBox.Show(
            $"Bạn có chắc muốn xóa khách hàng {selectedCustomer.Code} - {selectedCustomer.Name}?",
            "Xác nhận xóa",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (confirm != MessageBoxResult.Yes)
        {
            SetStatus("Đã hủy xóa khách hàng", Brushes.SlateGray);
            return;
        }

        _customerService.DeleteCustomer(selectedCustomer.Id);
        LoadCustomers();
        SelectedCustomer = null;
        SetStatus($"Đã xóa khách hàng: {selectedCustomer.Code}", Brushes.IndianRed);
    }

    private bool FilterCustomer(object item)
    {
        if (item is not Customer customer)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(SearchText))
        {
            return true;
        }

        var search = SearchText.Trim();

        return customer.Code.Contains(search, StringComparison.OrdinalIgnoreCase)
            || customer.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
            || customer.Phone.Contains(search, StringComparison.OrdinalIgnoreCase)
            || customer.Email.Contains(search, StringComparison.OrdinalIgnoreCase);
    }
}