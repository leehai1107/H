using CommunityToolkit.Mvvm.Input;
using ERP.Desktop.Services;
using ERP.Application.Customers;
using ERP.Application.Inventory;
using ERP.Application.Purchases;
using ERP.Infrastructure.Customers;
using ERP.Infrastructure.Inventory;
using ERP.Infrastructure.Purchases;

namespace ERP.Desktop.ViewModels;

public enum NavigationItem
{
    Dashboard,
    Customer,
    Inventory,
    Purchase,
    Production,
    Settings
}

public class MainWindowViewModel : ViewModelBase
{
    private object? _currentView;
    private NavigationItem _currentNavigationItem;
    private ShellPageViewModelBase? _currentPageContext;
    private readonly IDialogService _dialogService;

    public IRelayCommand ShowDashboardCommand { get; }
    public IRelayCommand ShowCustomerCommand { get; }
    public IRelayCommand ShowInventoryCommand { get; }
    public IRelayCommand ShowPurchaseCommand { get; }
    public IRelayCommand ShowProductionCommand { get; }
    public IRelayCommand ShowSettingsCommand { get; }

    public NavigationItem CurrentNavigationItem
    {
        get => _currentNavigationItem;
        private set
        {
            _currentNavigationItem = value;
            OnPropertyChanged();
        }
    }

    public object? CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public ShellPageViewModelBase? CurrentPageContext
    {
        get => _currentPageContext;
        private set
        {
            _currentPageContext = value;
            OnPropertyChanged();
        }
    }

    public MainWindowViewModel()
    {
        _dialogService = new DialogService();

        NavigateTo(new DashboardViewModel(), NavigationItem.Dashboard);

        ShowDashboardCommand = new RelayCommand(() => NavigateTo(new DashboardViewModel(), NavigationItem.Dashboard));
        ShowCustomerCommand = new RelayCommand(() => NavigateTo(new CustomerViewModel(new CustomerService(new InMemoryCustomerRepository()), _dialogService), NavigationItem.Customer));
        ShowInventoryCommand = new RelayCommand(() => NavigateTo(new InventoryViewModel(new InventoryService(new InMemoryInventoryRepository()), _dialogService), NavigationItem.Inventory));
        ShowPurchaseCommand = new RelayCommand(() => NavigateTo(new PurchaseViewModel(new PurchaseService(new InMemoryPurchaseRepository()), _dialogService), NavigationItem.Purchase));
        ShowProductionCommand = new RelayCommand(() => NavigateTo(new ProductionViewModel(), NavigationItem.Production));
        ShowSettingsCommand = new RelayCommand(() => NavigateTo(new SettingsViewModel(), NavigationItem.Settings));
    }

    private void NavigateTo(ShellPageViewModelBase viewModel, NavigationItem navigationItem)
    {
        CurrentView = viewModel;
        CurrentPageContext = viewModel;
        CurrentNavigationItem = navigationItem;
    }
}