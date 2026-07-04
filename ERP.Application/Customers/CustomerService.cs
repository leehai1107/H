using ERP.Domain.Customers;

namespace ERP.Application.Customers;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public IReadOnlyList<Customer> GetCustomers()
    {
        return _customerRepository.GetAll();
    }

    public Customer AddCustomer(Customer customer)
    {
        return _customerRepository.Add(customer);
    }

    public void UpdateCustomer(Customer customer)
    {
        _customerRepository.Update(customer);
    }

    public void DeleteCustomer(int customerId)
    {
        _customerRepository.Delete(customerId);
    }
}