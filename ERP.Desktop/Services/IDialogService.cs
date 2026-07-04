using ERP.Domain.Customers;
using ERP.Domain.Inventory;
using ERP.Domain.Purchases;

namespace ERP.Desktop.Services;

public interface IDialogService
{
    Customer? ShowCustomerEditor(Customer? customer = null);

    InventoryItem? ShowInventoryEditor(InventoryItem? item = null);

    PurchaseOrder? ShowPurchaseEditor(PurchaseOrder? order = null);
}