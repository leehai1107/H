using ERP.Domain.Customers;

namespace ERP.Application.Customers;

public interface ICustomerRepository
{
    IReadOnlyList<Customer> GetAll();

    Customer Add(Customer customer);

    void Update(Customer customer);

    void Delete(int customerId);
}