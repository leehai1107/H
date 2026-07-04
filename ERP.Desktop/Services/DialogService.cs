using System.Windows;
using ERP.Desktop.ViewModels;
using ERP.Desktop.Views.Customer;
using ERP.Desktop.Views.Inventory;
using ERP.Desktop.Views.Purchase;
using ERP.Domain.Customers;
using ERP.Domain.Inventory;
using ERP.Domain.Purchases;

namespace ERP.Desktop.Services;

public class DialogService : IDialogService
{
    public Customer? ShowCustomerEditor(Customer? customer = null)
    {
        var viewModel = new CustomerEditorViewModel(customer);
        var window = new CustomerEditorWindow(viewModel)
        {
            Owner = System.Windows.Application.Current.MainWindow
        };

        return window.ShowDialog() == true ? viewModel.Result : null;
    }

    public InventoryItem? ShowInventoryEditor(InventoryItem? item = null)
    {
        var viewModel = new InventoryEditorViewModel(item);
        var window = new InventoryEditorWindow(viewModel)
        {
            Owner = System.Windows.Application.Current.MainWindow
        };

        return window.ShowDialog() == true ? viewModel.Result : null;
    }

    public PurchaseOrder? ShowPurchaseEditor(PurchaseOrder? order = null)
    {
        var viewModel = new PurchaseEditorViewModel(order);
        var window = new PurchaseEditorWindow(viewModel)
        {
            Owner = System.Windows.Application.Current.MainWindow
        };

        return window.ShowDialog() == true ? viewModel.Result : null;
    }
}