using ERP.Domain.Customers;

namespace ERP.Application.Customers;

public interface ICustomerService
{
    IReadOnlyList<Customer> GetCustomers();

    Customer AddCustomer(Customer customer);

    void UpdateCustomer(Customer customer);

    void DeleteCustomer(int customerId);
}