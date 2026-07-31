using Application.DTOs.Orders;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // GET: api/orders
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    // POST: api/orders
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequestDto request)
    {
        try
        {
            var orderId = await _orderService.CreateOrderAsync(request);
            return Ok(new { Message = "Sipariş başarıyla oluşturuldu.", OrderId = orderId });
        }
        catch (Exception ex)
        {
            // Stok yetersizliği veya müşteri bulunamaması gibi Service'ten fırlatılan hataları burada yakalıyoruz
            return BadRequest(new { Message = ex.Message });
        }
    }
}