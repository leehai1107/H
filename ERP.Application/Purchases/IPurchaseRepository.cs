using ERP.Domain.Purchases;

namespace ERP.Application.Purchases;

public interface IPurchaseRepository
{
    IReadOnlyList<PurchaseOrder> GetAll();

    PurchaseOrder Add(PurchaseOrder order);

    void Update(PurchaseOrder order);

    void Delete(int orderId);
}
