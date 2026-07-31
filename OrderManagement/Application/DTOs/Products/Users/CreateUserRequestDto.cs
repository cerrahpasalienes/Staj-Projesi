namespace Application.DTOs.Users;

public class CreateUserRequestDto
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; // Şifreyi açık metin alıp Service'te şifreleyeceğiz
    public string PhoneNumber { get; set; } = string.Empty;
}