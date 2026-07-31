namespace Application.DTOs.Users;

public class LoginResponseDto
{
    public int UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}