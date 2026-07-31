using Application.DTOs.Users;

namespace Application.Interfaces.Services;

public interface IUserService
{
    Task<int> CreateUserAsync(CreateUserRequestDto request);
    Task<List<UserDto>> GetAllUsersAsync();
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request); // YENİ EKLENEN
}