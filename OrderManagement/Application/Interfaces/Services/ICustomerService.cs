using Application.DTOs.Customers;

namespace Application.Interfaces.Services;

public interface ICustomerService
{
    Task<IReadOnlyList<CustomerDto>> GetAllCustomersAsync();
    Task<CustomerDto?> GetCustomerByIdAsync(int id);
    Task<int> CreateCustomerAsync(CreateCustomerRequestDto request);
    Task UpdateCustomerAsync(int id, UpdateCustomerRequestDto request);
    Task DeleteCustomerAsync(int id);
}