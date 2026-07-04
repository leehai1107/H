using ERP.Application.Customers;
using ERP.Domain.Customers;

namespace ERP.Infrastructure.Customers;

public class InMemoryCustomerRepository : ICustomerRepository
{
    private static readonly List<Customer> Customers = new()
    {
        new Customer
        {
            Id = 1,
            Code = "CUS-001",
            Name = "Công ty TNHH Minh Phát",
            Phone = "0901 111 222",
            Email = "contact@minhphat.vn",
            Address = "KCN Tân Tạo, TP.HCM",
            IsActive = true
        },
        new Customer
        {
            Id = 2,
            Code = "CUS-002",
            Name = "Công ty CP An Khang",
            Phone = "0902 333 444",
            Email = "sales@ankhang.vn",
            Address = "Bình Dương",
            IsActive = true
        },
        new Customer
        {
            Id = 3,
            Code = "CUS-003",
            Name = "Xưởng Cơ Khí Hòa Bình",
            Phone = "0903 555 666",
            Email = "info@hoabinh.com",
            Address = "Đồng Nai",
            IsActive = false
        }
    };

    private static int _nextId = Customers.Count + 1;

    public IReadOnlyList<Customer> GetAll()
    {
        return Customers;
    }

    public Customer Add(Customer customer)
    {
        var createdCustomer = new Customer
        {
            Id = _nextId++,
            Code = customer.Code,
            Name = customer.Name,
            Phone = customer.Phone,
            Email = customer.Email,
            Address = customer.Address,
            IsActive = customer.IsActive
        };

        Customers.Add(createdCustomer);
        return createdCustomer;
    }

    public void Update(Customer customer)
    {
        var existingCustomer = Customers.FirstOrDefault(item => item.Id == customer.Id);
        if (existingCustomer is null)
        {
            return;
        }

        existingCustomer.Code = customer.Code;
        existingCustomer.Name = customer.Name;
        existingCustomer.Phone = customer.Phone;
        existingCustomer.Email = customer.Email;
        existingCustomer.Address = customer.Address;
        existingCustomer.IsActive = customer.IsActive;
    }

    public void Delete(int customerId)
    {
        var existingCustomer = Customers.FirstOrDefault(item => item.Id == customerId);
        if (existingCustomer is null)
        {
            return;
        }

        Customers.Remove(existingCustomer);
    }
}