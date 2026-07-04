using ERP.Domain.Inventory;

namespace ERP.Application.Inventory;

public class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _inventoryRepository;

    public InventoryService(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public IReadOnlyList<InventoryItem> GetInventoryItems()
    {
        return _inventoryRepository.GetAll();
    }

    public InventoryItem AddInventoryItem(InventoryItem item)
    {
        return _inventoryRepository.Add(item);
    }

    public void UpdateInventoryItem(InventoryItem item)
    {
        _inventoryRepository.Update(item);
    }

    public void DeleteInventoryItem(int itemId)
    {
        _inventoryRepository.Delete(itemId);
    }
}