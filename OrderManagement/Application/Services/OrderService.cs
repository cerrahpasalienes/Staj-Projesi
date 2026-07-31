using Application.DTOs.Orders;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly ICommandRepository<Order> _orderCommandRepository;
    private readonly IQueryRepository<Order> _orderQueryRepository;
    private readonly IQueryRepository<Product> _productQueryRepository;
    private readonly ICommandRepository<Product> _productCommandRepository;
    private readonly IQueryRepository<Customer> _customerQueryRepository;

    public OrderService(
        ICommandRepository<Order> orderCommandRepository,
        IQueryRepository<Order> orderQueryRepository,
        IQueryRepository<Product> productQueryRepository,
        ICommandRepository<Product> productCommandRepository,
        IQueryRepository<Customer> customerQueryRepository)
    {
        _orderCommandRepository = orderCommandRepository;
        _orderQueryRepository = orderQueryRepository;
        _productQueryRepository = productQueryRepository;
        _productCommandRepository = productCommandRepository;
        _customerQueryRepository = customerQueryRepository;
    }

    public async Task<IReadOnlyList<OrderListDto>> GetAllOrdersAsync()
    {
        var orders = await _orderQueryRepository.GetAllAsync();
        return orders.Select(o => new OrderListDto
        {
            Id = o.Id,
            OrderNumber = o.OrderNumber,
            CustomerId = o.CustomerId,
            OrderDate = o.OrderDate,
            TotalAmount = o.TotalAmount
        }).ToList();
    }

    public async Task<int> CreateOrderAsync(CreateOrderRequestDto request)
    {
        // 1. Müşteri kontrolü (Müşteri var mı?)
        var customer = await _customerQueryRepository.GetByIdAsync(request.CustomerId);
        if (customer == null) throw new Exception("Müşteri bulunamadı!");

        // 2. Sipariş nesnesini hazırlama
        var order = new Order
        {
            OrderNumber = "ORD-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
            CustomerId = request.CustomerId,
            OrderStatusId = 1, // 1: Pending (Bekliyor) durumu
            OrderDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            OrderItems = new List<OrderItem>()
        };

        decimal totalAmount = 0;

        // 3. Ürünlerin stok durumunu kontrol etme, fiyatı alma ve stoğu düşme
        foreach (var item in request.Items)
        {
            var product = await _productQueryRepository.GetByIdAsync(item.ProductId);
            if (product == null) throw new Exception($"Ürün bulunamadı (ID: {item.ProductId})");
            
            if (product.StockQuantity < item.Quantity) 
                throw new Exception($"Yetersiz stok! Ürün: {product.Name}");

            // Stok düşme işlemi
            product.StockQuantity -= item.Quantity;
            await _productCommandRepository.UpdateAsync(product); 

            // Fiyat hesaplama
            var totalPrice = product.Price * item.Quantity;
            totalAmount += totalPrice;

            // Sipariş kalemini ekleme (Dokümandaki kritik kural: O anki fiyat sabitlenir)
            order.OrderItems.Add(new OrderItem
            {
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price, 
                TotalPrice = totalPrice
            });
        }

        order.TotalAmount = totalAmount;

        // 4. Siparişi veritabanına kaydetme
        var createdOrder = await _orderCommandRepository.AddAsync(order);
        return createdOrder.Id;
    }
}