namespace Application.DTOs.Orders;

public class CreateOrderRequestDto
{
    public int CustomerId { get; set; }
    
    // Bir siparişte birden fazla ürün olabilir
    public List<CreateOrderItemDto> Items { get; set; } = new();
}

public class CreateOrderItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}