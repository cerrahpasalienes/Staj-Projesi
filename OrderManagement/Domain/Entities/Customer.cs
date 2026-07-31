namespace Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    
    // User tablosu ile ilişki (1 Müşteri 1 Kullanıcıdır)
    public int UserId { get; set; }
    public User User { get; set; } = null!; 

    public string CustomerCode { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

