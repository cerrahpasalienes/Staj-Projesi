using Application.DTOs.Customers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.Services;

public class CustomerService : ICustomerService
{
    private readonly IQueryRepository<Customer> _queryRepository;
    private readonly ICommandRepository<Customer> _commandRepository;

    public CustomerService(IQueryRepository<Customer> queryRepository, ICommandRepository<Customer> commandRepository)
    {
        _queryRepository = queryRepository;
        _commandRepository = commandRepository;
    }

    public async Task<IReadOnlyList<CustomerDto>> GetAllCustomersAsync()
    {
        var customers = await _queryRepository.GetAllAsync();
        return customers.Select(c => new CustomerDto
        {
            Id = c.Id,
            CustomerCode = c.CustomerCode,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber,
            IsActive = c.IsActive
        }).ToList();
    }

    public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
    {
        var customer = await _queryRepository.GetByIdAsync(id);
        if (customer == null) return null;

        return new CustomerDto
        {
            Id = customer.Id,
            CustomerCode = customer.CustomerCode,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            IsActive = customer.IsActive
        };
    }

    public async Task<int> CreateCustomerAsync(CreateCustomerRequestDto request)
    {
        var customer = new Customer
        {
            UserId = request.UserId, // DTO'dan gelen UserId'yi Entity'e aktardığımız kısım
            CustomerCode = request.CustomerCode,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var createdCustomer = await _commandRepository.AddAsync(customer);
        return createdCustomer.Id;
    }

    public async Task UpdateCustomerAsync(int id, UpdateCustomerRequestDto request)
    {
        var customer = await _queryRepository.GetByIdAsync(id);
        if (customer == null) throw new Exception("Müşteri bulunamadı!");

        customer.FirstName = request.FirstName;
        customer.LastName = request.LastName;
        customer.Email = request.Email;
        customer.PhoneNumber = request.PhoneNumber;
        customer.IsActive = request.IsActive;
        customer.UpdatedAt = DateTime.UtcNow;

        await _commandRepository.UpdateAsync(customer);
    }

    public async Task DeleteCustomerAsync(int id)
    {
        var customer = await _queryRepository.GetByIdAsync(id);
        if (customer == null) throw new Exception("Müşteri bulunamadı!");

        await _commandRepository.DeleteAsync(customer);
    }
}