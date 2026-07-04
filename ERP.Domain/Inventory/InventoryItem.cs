namespace ERP.Domain.Inventory;

public class InventoryItem
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Unit { get; set; } = string.Empty;

    public decimal Quantity { get; set; }

    public decimal MinQuantity { get; set; }

    public bool IsActive { get; set; } = true;
}