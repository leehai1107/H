using ERP.Domain.Inventory;

namespace ERP.Application.Inventory;

public interface IInventoryRepository
{
    IReadOnlyList<InventoryItem> GetAll();

    InventoryItem Add(InventoryItem item);

    void Update(InventoryItem item);

    void Delete(int itemId);
}