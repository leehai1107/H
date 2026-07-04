using ERP.Domain.Purchases;

namespace ERP.Application.Purchases;

public class PurchaseService : IPurchaseService
{
    private readonly IPurchaseRepository _purchaseRepository;

    public PurchaseService(IPurchaseRepository purchaseRepository)
    {
        _purchaseRepository = purchaseRepository;
    }

    public IReadOnlyList<PurchaseOrder> GetPurchaseOrders()
    {
        return _purchaseRepository.GetAll();
    }

    public PurchaseOrder AddPurchaseOrder(PurchaseOrder order)
    {
        return _purchaseRepository.Add(order);
    }

    public void UpdatePurchaseOrder(PurchaseOrder order)
    {
        _purchaseRepository.Update(order);
    }

    public void DeletePurchaseOrder(int orderId)
    {
        _purchaseRepository.Delete(orderId);
    }
}
