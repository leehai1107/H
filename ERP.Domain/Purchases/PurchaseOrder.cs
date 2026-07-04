using System;

namespace ERP.Domain.Purchases;

public class PurchaseOrder
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string SupplierName { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; } = DateTime.Today;

    public decimal Amount { get; set; }

    public string Status { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}
