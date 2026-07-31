namespace Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    
    // Müşteri ile ilişki (1 Siparişin 1 Müşterisi olur)
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    
    // Sipariş Durumu
    public int OrderStatusId { get; set; }
    
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // 1 Siparişin birden fazla kalemi (ürünü) olabilir
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}