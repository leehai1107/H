using ERP.Domain.Purchases;

namespace ERP.Application.Purchases;

public interface IPurchaseService
{
    IReadOnlyList<PurchaseOrder> GetPurchaseOrders();

    PurchaseOrder AddPurchaseOrder(PurchaseOrder order);

    void UpdatePurchaseOrder(PurchaseOrder order);

    void DeletePurchaseOrder(int orderId);
}
