using ERP.Domain.Inventory;

namespace ERP.Application.Inventory;

public interface IInventoryService
{
    IReadOnlyList<InventoryItem> GetInventoryItems();

    InventoryItem AddInventoryItem(InventoryItem item);

    void UpdateInventoryItem(InventoryItem item);

    void DeleteInventoryItem(int itemId);
}