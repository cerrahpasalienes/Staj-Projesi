namespace Application.DTOs.Customers;

public class CreateCustomerRequestDto
{
    public int UserId { get; set; } // Veritabanının istediği zorunlu alan
    public string CustomerCode { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}