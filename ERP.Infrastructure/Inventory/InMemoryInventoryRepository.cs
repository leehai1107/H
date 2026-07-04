using ERP.Application.Inventory;
using ERP.Domain.Inventory;

namespace ERP.Infrastructure.Inventory;

public class InMemoryInventoryRepository : IInventoryRepository
{
    private static readonly List<InventoryItem> Items = new()
    {
        new InventoryItem
        {
            Id = 1,
            Code = "INV-001",
            Name = "Tôn cuộn",
            Unit = "Kg",
            Quantity = 1250,
            MinQuantity = 300,
            IsActive = true
        },
        new InventoryItem
        {
            Id = 2,
            Code = "INV-002",
            Name = "Ống thép",
            Unit = "Cây",
            Quantity = 480,
            MinQuantity = 120,
            IsActive = true
        },
        new InventoryItem
        {
            Id = 3,
            Code = "INV-003",
            Name = "Mặt bích",
            Unit = "Bộ",
            Quantity = 64,
            MinQuantity = 25,
            IsActive = false
        }
    };

    private static int _nextId = Items.Count + 1;

    public IReadOnlyList<InventoryItem> GetAll()
    {
        return Items;
    }

    public InventoryItem Add(InventoryItem item)
    {
        var createdItem = new InventoryItem
        {
            Id = _nextId++,
            Code = item.Code,
            Name = item.Name,
            Unit = item.Unit,
            Quantity = item.Quantity,
            MinQuantity = item.MinQuantity,
            IsActive = item.IsActive
        };

        Items.Add(createdItem);
        return createdItem;
    }

    public void Update(InventoryItem item)
    {
        var existingItem = Items.FirstOrDefault(value => value.Id == item.Id);
        if (existingItem is null)
        {
            return;
        }

        existingItem.Code = item.Code;
        existingItem.Name = item.Name;
        existingItem.Unit = item.Unit;
        existingItem.Quantity = item.Quantity;
        existingItem.MinQuantity = item.MinQuantity;
        existingItem.IsActive = item.IsActive;
    }

    public void Delete(int itemId)
    {
        var existingItem = Items.FirstOrDefault(value => value.Id == itemId);
        if (existingItem is null)
        {
            return;
        }

        Items.Remove(existingItem);
    }
}