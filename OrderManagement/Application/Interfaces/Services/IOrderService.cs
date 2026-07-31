using Application.DTOs.Orders;

namespace Application.Interfaces.Services;

public interface IOrderService
{
    Task<int> CreateOrderAsync(CreateOrderRequestDto request);
    Task<IReadOnlyList<OrderListDto>> GetAllOrdersAsync();
}