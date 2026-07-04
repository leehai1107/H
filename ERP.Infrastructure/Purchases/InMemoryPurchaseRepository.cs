using ERP.Application.Purchases;
using ERP.Domain.Purchases;

namespace ERP.Infrastructure.Purchases;

public class InMemoryPurchaseRepository : IPurchaseRepository
{
    private static readonly List<PurchaseOrder> Orders = new()
    {
        new PurchaseOrder
        {
            Id = 1,
            Code = "PO-001",
            SupplierName = "Công ty Thép Bắc Nam",
            OrderDate = DateTime.Today.AddDays(-8),
            Amount = 154_000_000,
            Status = "Mới",
            IsActive = true
        },
        new PurchaseOrder
        {
            Id = 2,
            Code = "PO-002",
            SupplierName = "Công ty Hợp Kim Việt",
            OrderDate = DateTime.Today.AddDays(-4),
            Amount = 86_500_000,
            Status = "Đang xử lý",
            IsActive = true
        },
        new PurchaseOrder
        {
            Id = 3,
            Code = "PO-003",
            SupplierName = "Nhà Cung Cấp Miền Nam",
            OrderDate = DateTime.Today.AddDays(-2),
            Amount = 39_200_000,
            Status = "Hoàn tất",
            IsActive = false
        }
    };

    private static int _nextId = Orders.Count + 1;

    public IReadOnlyList<PurchaseOrder> GetAll()
    {
        return Orders;
    }

    public PurchaseOrder Add(PurchaseOrder order)
    {
        var createdOrder = new PurchaseOrder
        {
            Id = _nextId++,
            Code = order.Code,
            SupplierName = order.SupplierName,
            OrderDate = order.OrderDate,
            Amount = order.Amount,
            Status = order.Status,
            IsActive = order.IsActive
        };

        Orders.Add(createdOrder);
        return createdOrder;
    }

    public void Update(PurchaseOrder order)
    {
        var existingOrder = Orders.FirstOrDefault(value => value.Id == order.Id);
        if (existingOrder is null)
        {
            return;
        }

        existingOrder.Code = order.Code;
        existingOrder.SupplierName = order.SupplierName;
        existingOrder.OrderDate = order.OrderDate;
        existingOrder.Amount = order.Amount;
        existingOrder.Status = order.Status;
        existingOrder.IsActive = order.IsActive;
    }

    public void Delete(int orderId)
    {
        var existingOrder = Orders.FirstOrDefault(value => value.Id == orderId);
        if (existingOrder is null)
        {
            return;
        }

        Orders.Remove(existingOrder);
    }
}
