using Application.DTOs.Users;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly ICommandRepository<User> _commandRepository;
    private readonly IQueryRepository<User> _queryRepository;

    public UserService(ICommandRepository<User> commandRepository, IQueryRepository<User> queryRepository)
    {
        _commandRepository = commandRepository;
        _queryRepository = queryRepository;
    }

    public async Task<int> CreateUserAsync(CreateUserRequestDto request)
    {
        // Şifreyi Hash'lemek için standart HMACSHA512 kullanıyoruz
        using var hmac = new HMACSHA512();

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
            PasswordSalt = hmac.Key,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var createdUser = await _commandRepository.AddAsync(user);
        return createdUser.Id;
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _queryRepository.GetAllAsync();
        return users.Select(u => new UserDto
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber
        }).ToList();
    }
}