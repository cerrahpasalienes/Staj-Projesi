namespace Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }
    
    // Hangi siparişe ait?
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    
    // Hangi ürün alındı?
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    
    public int Quantity { get; set; }
    
    // Dokümanda istenen kritik kural: Sipariş anındaki sabit fiyatı burada tutuyoruz
    public decimal UnitPrice { get; set; } 
    public decimal TotalPrice { get; set; }
}